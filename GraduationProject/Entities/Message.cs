namespace GraduationProject.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public string Content { get; set; } = string.Empty;

    /// <summary>"User" or "Bot"</summary>
    public string Sender { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Conversation Conversation { get; set; } = null!;
}