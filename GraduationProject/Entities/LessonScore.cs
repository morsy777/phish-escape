namespace GraduationProject.Entities;

public sealed class LessonScore
{
    public int LessonScoreId { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int LessonId { get; set; } = default!;
    public Lesson Lesson { get; set; } = null!;

    public int Score { get; set; } = default!;
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}
