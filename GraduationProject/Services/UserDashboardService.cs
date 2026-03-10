namespace GraduationProject.Services;

public class UserDashboardService(ApplicationDbContext context) : IUserDashboardService
{
    private readonly ApplicationDbContext _context = context;
    private static string _performance = "Excellent";
    private static string _league = "Gold";
    private static int _passingScore = 70;

    public async Task<Result<UserDashboardResponseDto>> GetDashboardAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
            return Result.Failure<UserDashboardResponseDto>(UserErrors.InvalidJwtToken);


        var userStats = await _context.UserStats
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (userStats is null)
            return Result.Failure<UserDashboardResponseDto>(UserDashboardErrors.UserStatsNotFound);


        var attempts = await _context.UserTestAttempts
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);

        var totalSimulations = attempts.Count;
        var passedSimulations = attempts.Count(x => x.Score >= _passingScore);


        var lessons = await _context.Lessons
            .OrderBy(x => x.OrderNumber)
            .Take(3)
            .ToListAsync(cancellationToken);


        var lessonIds = lessons.Select(x => x.LessonId).ToList();


        var questionsPerLesson = await _context.Questions
            .Where(x => lessonIds.Contains(x.LessonId))
            .GroupBy(x => x.LessonId)
            .Select(g => new
            {
                LessonId = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(x => x.LessonId, x => x.Count, cancellationToken);


        var answersPerLesson = await _context.UserAnswers
            .Where(x => x.UserId == userId && lessonIds.Contains(x.Question.LessonId))
            .GroupBy(x => x.Question.LessonId)
            .Select(g => new
            {
                LessonId = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(x => x.LessonId, x => x.Count, cancellationToken);


        var activeLessons = lessons.Select(lesson =>
        {
            var totalQuestions = questionsPerLesson.GetValueOrDefault(lesson.LessonId);
            var answeredQuestions = answersPerLesson.GetValueOrDefault(lesson.LessonId);

            var progress = totalQuestions == 0
                ? 0
                : (int)((double)answeredQuestions / totalQuestions * 100);

            return new ActiveLessonDto
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                Progress = progress,
                Locked = false
            };
        }).ToList();


        var response = new UserDashboardResponseDto
        {
            SecurityScore = new SecurityScoreDto
            {
                Score = userStats.SecurityScore,
                DetectionAccuracy = userStats.DetectionAccuracy,
                Performance = _performance
            },

            Simulations = new SimulationStatsDto
            {
                Passed = passedSimulations,
                Total = totalSimulations
            },

            Rank = new RankDto
            {
                GlobalRank = userStats.GlobalRank,
                League = _league
            },

            Streak = new StreakDto
            {
                CurrentStreak = user.CurrentStreak,
                MaxStreak = user.MaxStreak
            },

            ActiveLessons = activeLessons
        };

        return Result.Success(response);
    }
}
