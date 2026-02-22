namespace GraduationProject.Contracts.Lesson;

public sealed class LessonResponseDto
{
    public int LessonId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int OrderNumber { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }
}