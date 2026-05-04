using GraduationProject.Contracts.ChatBot;

namespace GraduationProject.Services;

public class ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger) : IExternalApiService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ExternalApiService> _logger = logger;

    // The trailing endpoint segment — change if the HF Space exposes a different route
    private const string ChatEndpoint = "/webhook";

    public async Task<string> SendMessageAsync(ExternalApiRequestDto request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(ChatEndpoint, request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ExternalApiResponseDto>();

            if (result is null || string.IsNullOrWhiteSpace(result.Response))
                throw new InvalidOperationException("External API returned an empty response.");

            return result.Response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error while calling external AI API.");
            throw new ApplicationException("Failed to reach the AI service. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling external AI API.");
            throw;
        }
    }
}