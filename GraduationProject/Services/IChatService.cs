using GraduationProject.Contracts.ChatBot;

namespace GraduationProject.Services;

public interface IChatService
{
    Task<ChatResponseDto> SendMessageAsync(ChatRequestDto request);
}
