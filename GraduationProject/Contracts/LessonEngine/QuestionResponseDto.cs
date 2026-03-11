namespace GraduationProject.Contracts.LessonEngine;

public sealed class QuestionResponseDto
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public string QuestionContent { get; set; } = string.Empty;

    public string? Explanation { get; set; }

    public QuestionType QuestionType { get; set; }

    public List<AnswerResponseDto> Answers { get; set; } = [];
}