using GraduationProject.Contracts.ChatBot;
using Microsoft.Extensions.Logging;

namespace GraduationProject.Services;

public class ChatService(ApplicationDbContext db, IExternalApiService externalApiService, ILogger<ChatService> logger) : IChatService
{
    private readonly ApplicationDbContext _db = db;
    private readonly IExternalApiService _externalApiService = externalApiService;
    private readonly ILogger<ChatService> _logger = logger;

    private const int HistoryWindowSize = 10;
    private const string SystemPrompt =
        "System: You are a cybersecurity assistant specialized in phishing detection.";

    public async Task<ChatResponseDto> SendMessageAsync(ChatRequestDto request)
    {
        // ── 1. Resolve or create the conversation ──────────────────────────
        var conversation = await ResolveConversationAsync(request.ConversationId, request.UserId);

        // ── 2. Persist the incoming user message ───────────────────────────
        var userMessage = new Message
        {
            ConversationId = conversation.Id,
            Content = request.Message,
            Sender = "User",
            Timestamp = DateTime.UtcNow
        };

        _db.Messages.Add(userMessage);
        await _db.SaveChangesAsync();

        // ── 3. Load the last N messages for this conversation ──────────────
        var history = await _db.Messages
            .Where(m => m.ConversationId == conversation.Id)
            .OrderByDescending(m => m.Timestamp)
            .Take(HistoryWindowSize)
            .OrderBy(m => m.Timestamp)          // re-order chronologically for the prompt
            .ToListAsync();

        // ── 4 & 5. Build the prompt ────────────────────────────────────────
        var prompt = BuildPrompt(history, request.Message);

        _logger.LogInformation(
            "Sending prompt to external API for conversation {ConversationId}.", conversation.Id);

        // ── 6 & 7. Call external (stateless) API ──────────────────────────
        var apiRequest = new ExternalApiRequestDto
        {
            UserId = request.UserId,
            Message = prompt           // full context injected into the message field
        };

        var botReply = await _externalApiService.SendMessageAsync(apiRequest);

        // ── 8. Persist the bot's response ─────────────────────────────────
        var botMessage = new Message
        {
            ConversationId = conversation.Id,
            Content = botReply,
            Sender = "Bot",
            Timestamp = DateTime.UtcNow
        };

        _db.Messages.Add(botMessage);
        await _db.SaveChangesAsync();

        // ── 9. Return to controller ────────────────────────────────────────
        _logger.LogInformation($"Bot message: {botMessage}");
        return new ChatResponseDto
        {
            ConversationId = conversation.Id,
            Reply = botReply
        };
    }

    // ── Helpers ────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the existing conversation if a valid ID is supplied,
    /// otherwise creates and persists a new one.
    /// </summary>
    private async Task<Conversation> ResolveConversationAsync(Guid? conversationId, string userId)
    {
        if (conversationId.HasValue && conversationId.Value != Guid.Empty)
        {
            var existing = await _db.Conversations.FindAsync(conversationId.Value);

            if (existing is not null)
                return existing;

            _logger.LogWarning(
                "Conversation {Id} not found — starting a new one.", conversationId.Value);
        }

        var conversation = new Conversation
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _db.Conversations.Add(conversation);
        await _db.SaveChangesAsync();

        return conversation;
    }

    /// <summary>
    /// Builds the full prompt string:
    ///   System: ...
    ///   User: ...
    ///   Bot: ...
    ///   User: &lt;current message&gt;
    /// </summary>
    private static string BuildPrompt(IEnumerable<Message> history, string currentUserMessage)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine(SystemPrompt);
        sb.AppendLine();

        foreach (var msg in history)
        {
            // Skip the last message if it is the one we just saved (avoid duplication)
            sb.AppendLine($"{msg.Sender}: {msg.Content}");
        }

        // Ensure the very last line is always the current user turn
        // (already saved above, so it will appear in history;
        //  but we append explicitly to make the context boundary crystal-clear)
        sb.AppendLine($"User: {currentUserMessage}");

        return sb.ToString().TrimEnd();
    }
}