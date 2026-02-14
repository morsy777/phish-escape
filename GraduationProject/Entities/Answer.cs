namespace GraduationProject.Entities;

public sealed class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; } = default!;
    public Question Question { get; set; } = null!;

    public string AnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = default!;

    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
}
