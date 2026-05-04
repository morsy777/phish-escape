namespace GraduationProject.Contracts.ChatBot;

using System.Text.Json.Serialization;

public class ExternalApiRequestDto
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}