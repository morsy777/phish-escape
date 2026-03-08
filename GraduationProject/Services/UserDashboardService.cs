namespace GraduationProject.Services;

public class UserDashboardService(ApplicationDbContext context) : IUserDashboardService
{
    private readonly ApplicationDbContext _context = context;
    private static string _performance = "Excellent";
    private static string _league = "Gold";

    public async Task<Result<UserDashboardResponseDto>> GetDashboardAsync(string userId, CancellationToken cancellationToken = default)
    {
        var isUserExist = await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);

        if (!isUserExist)
            return Result.Failure<UserDashboardResponseDto>(UserErrors.InvalidJwtToken);

        var userStats = await _context.UserStats
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (userStats is null)
            return Result.Failure<UserDashboardResponseDto>(UserDashboardErrors.UserStatsNotFound);


        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
            return Result.Failure<UserDashboardResponseDto>(UserErrors.UserNotFound);


        var attempts = await _context.UserTestAttempts
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);

        var totalSimulations = attempts.Count;
        var passedSimulations = attempts.Count(x => x.Score >= 70);


        var lessons = await _context.Lessons
            .OrderBy(x => x.OrderNumber)
            .Take(3)
            .ToListAsync(cancellationToken);

        var userLessons = await _context.LessonScores
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);

        // progress = answeredQuestions / totalQuestions * 100
        var activeLessons = new List<ActiveLessonDto>();

        foreach (var lesson in lessons)
        {
            var totalQuestions = await _context.Questions
                .CountAsync(x => x.LessonId == lesson.LessonId, cancellationToken);

            var answeredQuestions = await _context.UserAnswers
                .CountAsync(x => x.UserId == userId && x.Question.LessonId == lesson.LessonId, cancellationToken);

            var progress = totalQuestions == 0
                ? 0
                : (int)((double)answeredQuestions / totalQuestions * 100);

            activeLessons.Add(new ActiveLessonDto
            {
                LessonId = lesson.LessonId,
                Title = lesson.Title,
                Progress = progress,
                Locked = false
            });
        }


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
