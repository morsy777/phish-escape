namespace GraduationProject.Services;

public interface ILessonEngineService
{
    Task<Result<List<QuestionResponseDto>>> GetLessonQuestionsAsync(
        int lessonId,
        CancellationToken cancellationToken = default);

    Task<Result<QuestionResponseDto>> GetNextQuestionAsync(
        int lessonId,
        string userId,
        CancellationToken cancellationToken = default);

    Task<Result<SubmitAnswerResponseDto>> SubmitAnswerAsync(
        int lessonId,
        string userId,
        SubmitAnswerRequestDto request,
        CancellationToken cancellationToken = default);

    Task<Result<LessonResultDto>> GetLessonResultAsync(
        int lessonId,
        string userId,
        CancellationToken cancellationToken = default);
}
