namespace GraduationProject.Entities;

public sealed class Lesson
{
    public int LessonId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string DifficultyLevel { get; set; } = string.Empty;

    public int OrderNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<LessonScore> LessonScores { get; set; } = [];
}
