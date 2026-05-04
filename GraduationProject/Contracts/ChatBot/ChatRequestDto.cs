namespace GraduationProject.Contracts.ChatBot;

public class ChatRequestDto
{
    /// <summary>
    /// Pass an existing Guid to continue a conversation,
    /// or Guid.Empty / null to start a new one.
    /// </summary>
    public Guid? ConversationId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Message { get; set; } = string.Empty;
}