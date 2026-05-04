using GraduationProject.Contracts.ChatBot;

namespace GraduationProject.Services;

public interface IExternalApiService
{
    /// <summary>
    /// Sends the fully-assembled prompt to the HuggingFace endpoint
    /// and returns the bot's raw text reply.
    /// </summary>
    Task<string> SendMessageAsync(ExternalApiRequestDto request);
}
