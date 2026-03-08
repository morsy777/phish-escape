namespace GraduationProject.Contracts.DashboardStats;

public class DashboardStatsResponse
{
    public int TotalLessons { get; set; }
    public int TotalQuestions { get; set; }

    public List<LessonQuestionsDto> QuestionsPerLesson { get; set; } = [];
    public List<DifficultyLessonsDto> LessonsPerDifficulty { get; set; } = [];
}