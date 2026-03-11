namespace GraduationProject.Services;

public sealed class LessonEngineService(ApplicationDbContext context)
    : ILessonEngineService
{
    private readonly ApplicationDbContext _context = context;
    private const int PassingScore = 70;

    public async Task<Result<List<QuestionResponseDto>>> GetLessonQuestionsAsync(
        int lessonId,
        CancellationToken cancellationToken = default)
    {
        var lessonExists = await _context.Lessons
            .AnyAsync(x => x.LessonId == lessonId, cancellationToken);

        if (!lessonExists)
            return Result.Failure<List<QuestionResponseDto>>(LessonErrors.NotFound);

        var questions = await _context.Questions
            .Where(x => x.LessonId == lessonId)
            .OrderBy(x => x.QuestionId)
            .Select(q => new QuestionResponseDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                QuestionContent = q.QuestionContent,
                Explanation = q.Explanation,
                QuestionType = q.QuestionType,
                Answers = q.Answers.Select(a => new AnswerResponseDto
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return Result.Success(questions);
    }

    public async Task<Result<QuestionResponseDto>> GetNextQuestionAsync(
        int lessonId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var lessonExists = await _context.Lessons
            .AnyAsync(x => x.LessonId == lessonId, cancellationToken);

        if (!lessonExists)
            return Result.Failure<QuestionResponseDto>(LessonErrors.NotFound);

        var answeredQuestionIds = await _context.UserAnswers
            .Where(x => x.UserId == userId)
            .Select(x => x.QuestionId)
            .ToListAsync(cancellationToken);

        var nextQuestion = await _context.Questions
            .Where(x =>
                x.LessonId == lessonId &&
                !answeredQuestionIds.Contains(x.QuestionId))
            .OrderBy(x => x.QuestionId)
            .Select(q => new QuestionResponseDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                QuestionContent = q.QuestionContent,
                Explanation = q.Explanation,
                QuestionType = q.QuestionType,
                Answers = q.Answers.Select(a => new AnswerResponseDto
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (nextQuestion is null)
            return Result.Failure<QuestionResponseDto>(LessonErrors.LessonCompleted);

        return Result.Success(nextQuestion);
    }


    public async Task<Result<SubmitAnswerResponseDto>> SubmitAnswerAsync(
        int lessonId,
        string userId,
        SubmitAnswerRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var lessonExists = await _context.Lessons
            .AnyAsync(x => x.LessonId == lessonId, cancellationToken);

        if (!lessonExists)
            return Result.Failure<SubmitAnswerResponseDto>(LessonErrors.NotFound);

        var question = await _context.Questions
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x =>
                x.QuestionId == request.QuestionId &&
                x.LessonId == lessonId,
                cancellationToken);

        if (question is null)
            return Result.Failure<SubmitAnswerResponseDto>(QuestionErrors.NotFound);

        var answer = question.Answers
            .FirstOrDefault(x => x.AnswerId == request.AnswerId);

        if (answer is null)
            return Result.Failure<SubmitAnswerResponseDto>(QuestionErrors.InvalidAnswerReference);

        var userAnswer = new UserAnswer
        {
            UserId = userId,
            QuestionId = request.QuestionId,
            AnswerId = request.AnswerId,
            IsCorrect = answer.IsCorrect
        };

        try
        {
            _context.UserAnswers.Add(userAnswer);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            return Result.Failure<SubmitAnswerResponseDto>(QuestionErrors.AlreadyAnswered);
        }

        await UpdateUserStatsAsync(userId, cancellationToken);

        return Result.Success(new SubmitAnswerResponseDto
        {
            IsCorrect = answer.IsCorrect,
            Explanation = question.Explanation,
            Message = answer.IsCorrect ? "Correct Answer" : "Wrong Answer"
        });
    }

    public async Task<Result<LessonResultDto>> GetLessonResultAsync(
        int lessonId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var questionIds = await _context.Questions
            .Where(x => x.LessonId == lessonId)
            .Select(x => x.QuestionId)
            .ToListAsync(cancellationToken);

        if (!questionIds.Any())
            return Result.Failure<LessonResultDto>(LessonErrors.NotFound);

        var answers = await _context.UserAnswers
            .Where(x =>
                x.UserId == userId &&
                questionIds.Contains(x.QuestionId))
            .ToListAsync(cancellationToken);

        var correct = answers.Count(x => x.IsCorrect);

        var total = questionIds.Count;

        var score = total == 0
            ? 0
            : (int)((double)correct / total * 100);

        var result = new LessonResultDto
        {
            TotalQuestions = total,
            CorrectAnswers = correct,
            WrongAnswers = total - correct,
            Score = score,
            Passed = score >= PassingScore
        };

        return Result.Success(result);
    }

    private async Task UpdateUserStatsAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        var totalAnswers = await _context.UserAnswers
            .Where(x => x.UserId == userId)
            .CountAsync(cancellationToken);

        var correctAnswers = await _context.UserAnswers
            .Where(x => x.UserId == userId && x.IsCorrect)
            .CountAsync(cancellationToken);

        if (totalAnswers == 0)
            return;

        var accuracy = (double)correctAnswers / totalAnswers * 100;

        var score = (int)Math.Round(accuracy);

        await _context.UserStats
            .Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(setters =>
                setters
                    .SetProperty(x => x.DetectionAccuracy, accuracy)
                    .SetProperty(x => x.SecurityScore, score),

                cancellationToken
            );
    }
}