namespace GraduationProject.Contracts.LessonEngine;

public sealed class AnswerResponseDto
{
    public int AnswerId { get; set; }

    public string AnswerText { get; set; } = string.Empty;
}