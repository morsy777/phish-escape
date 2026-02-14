namespace GraduationProject.Entities;

public sealed class UserTestAnswer
{
    public int UserTestAnswerId { get; set; }

    public int UserTestAttemptId { get; set; } = default!;
    public UserTestAttempt UserTestAttempt { get; set; } = null!;

    public int TestItemId { get; set; } = default!;
    public TestItem TestItem { get; set; } = null!;

    public int TestItemAnswerId { get; set; } = default!;
    public TestItemAnswer TestItemAnswer { get; set; } = null!;

    public bool IsCorrect { get; set; } = default!;
    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
}
