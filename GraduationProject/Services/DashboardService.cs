namespace GraduationProject.Services;

public class DashboardService(ApplicationDbContext context) : IDashboardService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<DashboardStatsResponse> GetDashboardStatsAsync()
    {
        var totalLessons = await _context.Lessons.CountAsync();

        var totalQuestions = await _context.Questions.CountAsync();

        var questionsPerLesson = await _context.Lessons
            .Select(l => new LessonQuestionsDto
            {
                LessonId = l.LessonId,
                Title = l.Title,
                QuestionsCount = _context.Questions
                    .Count(q => q.LessonId == l.LessonId)
            })
            .ToListAsync();

        var lessonsPerDifficulty = await _context.Lessons
            .GroupBy(l => l.DifficultyLevel)
            .Select(g => new DifficultyLessonsDto
            {
                DifficultyLevel = g.Key.ToString(),
                LessonsCount = g.Count()
            })
            .ToListAsync();

        return new DashboardStatsResponse
        {
            TotalLessons = totalLessons,
            TotalQuestions = totalQuestions,
            QuestionsPerLesson = questionsPerLesson,
            LessonsPerDifficulty = lessonsPerDifficulty
        };
    }
}