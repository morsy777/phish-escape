namespace GraduationProject.Services;

public class AdminLessonService(ApplicationDbContext context) : IAdminLessonService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<LessonResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var lessons = await _context.Lessons
            .ProjectToType<LessonResponseDto>()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<LessonResponseDto>>(lessons);
    }

    public async Task<Result<LessonResponseDto>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var lesson = await _context.Lessons
            .Where(x => x.LessonId == id)
            .ProjectToType<LessonResponseDto>()
            .FirstOrDefaultAsync(cancellationToken);

        if (lesson is null)
            return Result.Failure<LessonResponseDto>(LessonErrors.NotFound);

        return Result.Success(lesson);
    }

    public async Task<Result<int>> CreateAsync(CreateLessonDto dto, CancellationToken cancellationToken = default)
    {
        var titleExists = await _context.Lessons
            .AnyAsync(x => x.Title == dto.Title, cancellationToken);

        if (titleExists)
            return Result.Failure<int>(LessonErrors.DuplicateTitle);

        var orderExists = await _context.Lessons
            .AnyAsync(x => x.OrderNumber == dto.OrderNumber, cancellationToken);

        if(orderExists)
            return Result.Failure<int>(LessonErrors.DuplicateOrderNumber);

        var lesson = dto.Adapt<Lesson>();

        _context.Add(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(lesson.LessonId);
    }

    public async Task<Result> UpdateAsync(int id, UpdateLessonDto dto, CancellationToken cancellationToken = default)
    {
        var lesson = await _context.Lessons.FindAsync(id, cancellationToken);

        if (lesson is null)
            return Result.Failure(LessonErrors.NotFound);

        var titleExists = await _context.Lessons
            .AnyAsync(x => x.Title == dto.Title && x.LessonId != id, cancellationToken);

        if (titleExists)
            return Result.Failure(LessonErrors.DuplicateTitle);

        var orderExists = await _context.Lessons
            .AnyAsync(x => x.OrderNumber == dto.OrderNumber && x.LessonId != id, cancellationToken);

        if (orderExists)
            return Result.Failure(LessonErrors.DuplicateOrderNumber);

        dto.Adapt(lesson);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var lesson = await _context.Lessons.FindAsync(id, cancellationToken);

        if (lesson is null)
            return Result.Failure(LessonErrors.NotFound);

        _context.Lessons.Remove(lesson);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
