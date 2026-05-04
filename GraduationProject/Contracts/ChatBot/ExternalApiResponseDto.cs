namespace GraduationProject.Contracts.ChatBot;

using System.Text.Json.Serialization;

public class ExternalApiResponseDto
{
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;
}