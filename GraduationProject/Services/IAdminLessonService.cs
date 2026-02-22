namespace GraduationProject.Services;

public interface IAdminLessonService
{
    Task<Result<IEnumerable<LessonResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<LessonResponseDto>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateAsync(CreateLessonDto dto, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, UpdateLessonDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
