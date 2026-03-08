namespace GraduationProject.Contracts.DashboardStats;

public class LessonQuestionsDto
{
    public int LessonId { get; set; }
    public string Title { get; set; } = default!;
    public int QuestionsCount { get; set; }
}