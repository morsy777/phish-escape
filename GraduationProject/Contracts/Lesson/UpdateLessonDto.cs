namespace GraduationProject.Contracts.Lesson;

public sealed class UpdateLessonDto
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int OrderNumber { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }
}