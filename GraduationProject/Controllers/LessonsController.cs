namespace GraduationProject.Controllers;

[Route("api/lessons")]
[ApiController]
public class LessonsController(ILessonService lessonService) : ControllerBase
{
    private readonly ILessonService _lessonService = lessonService;

    [HttpGet]
    public async Task<IActionResult> GetLessons(CancellationToken cancellationToken)
    {
        var result = await _lessonService.GetLessonsAsync(cancellationToken);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("counts")]
    public async Task<IActionResult> GetLessonCounts(
    CancellationToken cancellationToken)
    {
        var result = await _lessonService
            .GetLessonCountsAsync(User.GetUserId()!, cancellationToken);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("active")]
    public async Task<IActionResult> GetActiveLesson(CancellationToken cancellationToken)
    {
        var result = await _lessonService
            .GetActiveLessonAsync(User.GetUserId()!, cancellationToken);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("cards")]
    public async Task<IActionResult> GetLessonCards(CancellationToken cancellationToken)
    {
        var lessons = await _lessonService.GetLessonsAsync(cancellationToken);
        var questions = await _lessonService.GetQuestionsCountPerEachLessonAsync(cancellationToken);
        var answers = await _lessonService.GetAnsweredQuestionsPerEachLessonAsync(User.GetUserId()!, cancellationToken);

        var cards = _lessonService.BuildLessonCards(
            lessons.Value,
            questions.Value,
            answers.Value);

        return Ok(cards.Value);
    }
}
