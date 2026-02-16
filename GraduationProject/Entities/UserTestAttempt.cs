namespace GraduationProject.Entities;

public sealed class UserTestAttempt
{
    public int UserTestAttemptId { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public int? Score { get; set; } = default!;
    public int TotalQuestions { get; set; } = default!;
    public double? Accuracy { get; set; } = default!;

    public ICollection<UserTestAnswer> UserTestAnswers { get; set; } = [];
}
