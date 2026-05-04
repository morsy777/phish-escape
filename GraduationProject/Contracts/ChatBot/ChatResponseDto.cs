namespace GraduationProject.Contracts.ChatBot;

public class ChatResponseDto
{
    public Guid ConversationId { get; set; }
    public string Reply { get; set; } = string.Empty;
}