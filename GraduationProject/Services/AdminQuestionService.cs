namespace GraduationProject.Services;

public sealed class AdminQuestionService(ApplicationDbContext context) : IAdminQuestionService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<AdminQuestionResponseDto>>> GetAllAsync(int lessonId, CancellationToken cancellationToken = default)
    {
        var questions = await _context.Questions
            .Where(x => x.LessonId == lessonId)
            .ProjectToType<AdminQuestionResponseDto>()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<AdminQuestionResponseDto>>(questions);
    }

    public async Task<Result<AdminQuestionResponseDto>> GetAsync(int lessonId, int questionId, CancellationToken cancellationToken = default)
    {
        var question = await _context.Questions
            .Where(x => x.LessonId == lessonId && x.QuestionId == questionId)
            .ProjectToType<AdminQuestionResponseDto>()
            .FirstOrDefaultAsync(cancellationToken);

        if (question is null)
            return Result.Failure<AdminQuestionResponseDto>(QuestionErrors.NotFound);

        return Result.Success(question);
    }

    public async Task<Result<int>> CreateAsync(int lessonId, CreateQuestionDto dto, CancellationToken cancellationToken = default)
    {
        var lessonExists = await _context.Lessons
            .AnyAsync(x => x.LessonId == lessonId, cancellationToken);

        if(!lessonExists)
            return Result.Failure<int>(QuestionErrors.LessonNotFound);

        var questionExists = await _context.Questions
            .AnyAsync(x => x.LessonId == lessonId && x.QuestionContent == dto.QuestionContent, cancellationToken);

        if(questionExists)
            return Result.Failure<int>(QuestionErrors.DuplicateQuestionContent);

        var question = dto.Adapt<Question>();
        question.LessonId = lessonId;

        _context.Questions.Add(question);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(question.QuestionId);

    }
    
    public async Task<Result> UpdateAsync(int lessonId, int questionId, UpdateQuestionDto dto, CancellationToken cancellationToken = default)
    {
        var lessonExists = await _context.Questions
            .AnyAsync(x => x.LessonId== lessonId, cancellationToken);

        if(!lessonExists)
            return Result.Failure(QuestionErrors.LessonNotFound);

        var question = await _context.Questions
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.LessonId == lessonId && x.QuestionId == questionId, cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.NotFound);

        var questionContentExists = await _context.Questions
            .AnyAsync(x =>
                x.QuestionContent == dto.QuestionContent &&
                x.LessonId == lessonId &&
                x.QuestionId != questionId,
                cancellationToken
            );

        if (questionContentExists)
            return Result.Failure(QuestionErrors.DuplicateQuestionContent);

        // update question scalar props
        question.QuestionText = dto.QuestionText;
        question.QuestionContent = dto.QuestionContent;
        question.QuestionType = dto.QuestionType;

        // existing answers in DB
        var existingAnswers = question.Answers.ToList();

        // incoming answers from dto
        var incomingAnswers = dto.Answers;

        var invalidIds = incomingAnswers
            .Where(x => x.AnswerId != 0 && 
                !question.Answers.Any(a => a.AnswerId == x.AnswerId))
            .Select(x => x.AnswerId)
            .ToList();

        if(invalidIds.Any())
            return Result.Failure(QuestionErrors.InvalidAnswerReference);

        /// Update & Remove
        foreach( var existing in existingAnswers )
        {
            var incoming = incomingAnswers
                .FirstOrDefault(x => x.AnswerId ==  existing.AnswerId);

            if(incoming is null)
            {
                // answer that removed from dto
                question.Answers.Remove(existing);
                continue;
            }

            existing.AnswerText = incoming.AnswerText;
            existing.IsCorrect = incoming.IsCorrect;
        }

        // Add new answer that exist in dto and don't exist in DB
        var newAnswers = dto.Answers
            .Where(x => x.AnswerId == 0);

        foreach( var newAnswer in newAnswers )
        {
            question.Answers.Add(new Answer
            {
                AnswerText = newAnswer.AnswerText,
                IsCorrect = newAnswer.IsCorrect,
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int lessonId, int questionId, CancellationToken cancellationToken = default)
    {
        var question = await _context.Questions
            .FirstOrDefaultAsync(x => x.QuestionId == questionId && x.LessonId == lessonId, cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.NotFound);

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
