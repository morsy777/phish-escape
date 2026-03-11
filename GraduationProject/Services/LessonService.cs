namespace GraduationProject.Services;

public class LessonService(ApplicationDbContext context) : ILessonService
{
    private readonly ApplicationDbContext _context = context;
    private const int _unlockThreshold = 70;

    // This Helper method, don't use mapping
    public async Task<Result<List<Lesson>>> GetLessonsAsync(CancellationToken cancellationToken)
    {
        var lessons = await _context.Lessons
            .OrderBy(x => x.OrderNumber)
            .Take(6)
            .ToListAsync(cancellationToken);

        return Result.Success(lessons);
    }

    public async Task<Result<List<LessonCountsDto>>> GetLessonCountsAsync(string userId, CancellationToken cancellationToken)
    {
        var questions = await _context.Questions
            .GroupBy(q => q.LessonId)
            .Select(g => new
            {
                LessonId = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);

        var answers = await _context.UserAnswers
            .Where(x => x.UserId == userId)
            .Join(
                _context.Questions,
                ua => ua.QuestionId,
                q => q.QuestionId,
                (ua, q) => new { q.LessonId })
            .GroupBy(x => x.LessonId)
            .Select(g => new
            {
                LessonId = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);

        var result = questions.Select(q => new LessonCountsDto
        {
            LessonId = q.LessonId,
            QuestionsCount = q.Count,
            AnswersCount = answers
                .FirstOrDefault(a => a.LessonId == q.LessonId)?.Count ?? 0
        }).ToList();

        return Result.Success(result);
    }

    // Helper method for BuildLessonCards
    public async Task<Result<Dictionary<int, int>>> GetQuestionsCountPerEachLessonAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Questions
            .GroupBy(x => x.LessonId)
            .Select(x => new
            {
                LessonId = x.Key,
                Count = x.Count()
            })
            .ToDictionaryAsync(x => x.LessonId, x => x.Count, cancellationToken);

        return Result.Success(result);
    }

    // Helper method for BuildLessonCards
    public async Task<Result<Dictionary<int, int>>> GetAnsweredQuestionsPerEachLessonAsync(string userId, CancellationToken cancellationToken)
    {
        var result = await _context.UserAnswers
            .Where(x => x.UserId == userId)
            .Join(
                _context.Questions,
                ua => ua.QuestionId,
                q => q.QuestionId,
                (ua, q) => new { q.LessonId })
            .GroupBy(x => x.LessonId)
            .Select(x => new
            {
                LessonId = x.Key,
                Count = x.Count()
            })
            .ToDictionaryAsync(x => x.LessonId, x => x.Count, cancellationToken);

        return Result.Success(result);
    }

    public Result<List<LessonDto>> BuildLessonCards(
        List<Lesson> lessons,
        Dictionary<int, int> questions,
        Dictionary<int, int> answers)
    {
        var result = new List<LessonDto>(lessons.Count);

        for (int i = 0; i < lessons.Count; i++)
        {
            var lesson = lessons[i];
            var lessonId = lesson.LessonId;

            var totalQuestions = questions.GetValueOrDefault(lessonId);
            var answeredQuestions = answers.GetValueOrDefault(lessonId);

            var progress = CalculateProgress(totalQuestions, answeredQuestions);
            var locked = IsLessonLocked(i, lessons, questions, answers);
            var status = CalculateLessonStatus(progress, locked);

            result.Add(new LessonDto
            {
                LessonId = lessonId,
                Title = lesson.Title,
                Progress = progress,
                Locked = locked,
                Status = status
            });
        }

        return Result.Success(result);
    }

    public async Task<Result<List<LessonDto>>> GetActiveLessonAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        var lessons = await _context.Lessons
            .OrderBy(x => x.OrderNumber)
            .Take(6)
            .ToListAsync(cancellationToken);

        var questions = await GetQuestionsCountPerEachLessonAsync(cancellationToken);
        var answers = await GetAnsweredQuestionsPerEachLessonAsync(userId, cancellationToken);

        var cards = BuildLessonCards(
            lessons,
            questions.Value,
            answers.Value);

        var active = cards.Value
            .Where(x => !x.Locked && x.Progress < 100)
            .OrderByDescending(x => x.Progress)
            .ToList();

        return Result.Success(active);
    }

    private int CalculateProgress(int total, int answered)
    {
        if (total == 0)
            return 0;

        return (int)((double)answered / total * 100);
    }

    private string CalculateLessonStatus(int progress, bool locked)
    {
        if (locked)
            return "Locked";

        if (progress > 0 && progress < 100)
            return "Active";

        if (progress == 0)
            return "NotStarted";

        return "Completed";
    }

    private bool IsLessonLocked(int lessonIndex, List<Lesson> lessons,
    Dictionary<int, int> questionsPerLesson,
    Dictionary<int, int> answersPerLesson)
    {
        if (lessonIndex == 0)
            return false;

        var previousLesson = lessons[lessonIndex - 1];

        var prevTotal = questionsPerLesson.GetValueOrDefault(previousLesson.LessonId);
        var prevAnswered = answersPerLesson.GetValueOrDefault(previousLesson.LessonId);

        var prevProgress = CalculateProgress(prevTotal, prevAnswered);

        return prevProgress < _unlockThreshold;
    }
}