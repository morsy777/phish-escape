namespace GraduationProject.Services;

public interface ILessonService
{
    Task<Result<List<Lesson>>> GetLessonsAsync(CancellationToken cancellationToken);
    Task<Result<List<LessonCountsDto>>> GetLessonCountsAsync(string userId, CancellationToken cancellationToken);
    Task<Result<Dictionary<int, int>>> GetQuestionsCountPerEachLessonAsync(CancellationToken cancellationToken);

    Task<Result<Dictionary<int, int>>> GetAnsweredQuestionsPerEachLessonAsync(string userId, CancellationToken cancellationToken);

    Result<List<LessonDto>> BuildLessonCards(
        List<Lesson> lessons,
        Dictionary<int, int> questions,
        Dictionary<int, int> answers);

    Task<Result<List<LessonDto>>> GetActiveLessonAsync(string userId, CancellationToken cancellationToken);
}