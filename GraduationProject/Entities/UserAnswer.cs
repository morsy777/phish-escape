namespace GraduationProject.Entities;

public sealed class UserAnswer
{
    public int UserAnswerId { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int QuestionId { get; set; } = default!;
    public Question Question { get; set; } = null!;

    public int AnswerId { get; set; } = default!;
    public Answer Answer { get; set; } = null!;

    public bool IsCorrect { get; set; } = default!;
    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
}
