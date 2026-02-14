namespace GraduationProject.Entities;

public sealed class TestItemAnswer
{
    public int TestItemAnswerId { get; set; }

    public int TestItemId { get; set; } = default!;
    public TestItem TestItem { get; set; } = null!;

    public string AnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = default!;

    public ICollection<UserTestAnswer> UserTestAnswers { get; set; } = [];
}

