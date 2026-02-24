namespace GraduationProject.Contracts.Question;

public sealed class AdminQuestionResponseDto
{
    public int QuestionId { get; set; }

    public int LessonId { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public string QuestionContent { get; set; } = string.Empty;

    public QuestionType QuestionType { get; set; }

    public List<AdminAnswerResponseDto> Answers { get; set; } = [];
}