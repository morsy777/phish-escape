using GraduationProject.Contracts.ChatBot;

namespace GraduationProject.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController(IChatService chatService, ILogger<ChatController> logger) : ControllerBase
{
    private readonly IChatService _chatService = chatService;
    private readonly ILogger<ChatController> _logger = logger;

    /// <summary>
    /// Send a user message and receive an AI-generated reply.
    /// Omit or pass Guid.Empty for ConversationId to start a new conversation.
    /// </summary>
    [HttpPost("send")]
    [ProducesResponseType(typeof(ChatResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> Send([FromBody] ChatRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _chatService.SendMessageAsync(request);
            return Ok(response);
        }
        catch (ApplicationException ex)
        {
            // Friendly error when the external AI service is unreachable
            _logger.LogWarning(ex, "External AI service error.");
            return StatusCode(StatusCodes.Status502BadGateway, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error in ChatController.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "An unexpected error occurred." });
        }
    }
}
