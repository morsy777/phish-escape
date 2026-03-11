namespace GraduationProject.Contracts.LessonEngine;

public sealed class SubmitAnswerResponseDto
{
    public bool IsCorrect { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Explanation { get; set; }
}