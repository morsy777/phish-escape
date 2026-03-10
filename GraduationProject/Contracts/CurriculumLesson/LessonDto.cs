namespace GraduationProject.Contracts.CurriculumLesson;

public class LessonDto
{
    public int LessonId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Progress { get; set; }

    public bool Locked { get; set; }

    public string Status { get; set; } = string.Empty;
}