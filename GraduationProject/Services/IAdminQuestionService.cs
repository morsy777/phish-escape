namespace GraduationProject.Services;

public interface IAdminQuestionService
{
    Task<Result<int>> CreateAsync(int lessonId, CreateQuestionDto dto, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int lessonId, int questionId, UpdateQuestionDto dto, CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(int lessonId, int questionId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<AdminQuestionResponseDto>>>
        GetAllAsync(int lessonId, CancellationToken cancellationToken = default);

    Task<Result<AdminQuestionResponseDto>>
        GetAsync(int lessonId, int questionId, CancellationToken cancellationToken = default);
}