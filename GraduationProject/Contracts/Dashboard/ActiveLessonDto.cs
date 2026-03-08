namespace GraduationProject.Contracts.Dashboard;

public class ActiveLessonDto
{
    public int LessonId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Progress { get; set; }

    public bool Locked { get; set; }
}