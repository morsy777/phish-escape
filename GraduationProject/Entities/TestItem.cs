namespace GraduationProject.Entities;

public sealed class TestItem
{
    public int TestItemId { get; set; }

    public string ContentType { get; set; } = default!;
    public string Content { get; set; } = string.Empty;

    public int OrderNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TestItemAnswer> TestItemAnswers { get; set; } = [];
    public ICollection<UserTestAnswer> UserTestAnswers { get; set; } = [];
}
