namespace GraduationProject.Controllers;

[Authorize]
[Route("api/lessons/{lessonId}")]
[ApiController]
public class LessonEngineController(ILessonEngineService service) : ControllerBase
{
    private readonly ILessonEngineService _service = service;

    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions(
        int lessonId,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetLessonQuestionsAsync(lessonId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("answer")]
    public async Task<IActionResult> SubmitAnswer(
        [FromRoute] int lessonId,
        [FromBody] SubmitAnswerRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _service.SubmitAnswerAsync(
            lessonId,
            User.GetUserId()!,
            request,
            cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("next-question")]
    public async Task<IActionResult> GetNextQuestion(
        [FromRoute] int lessonId,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetNextQuestionAsync(
            lessonId,
            User.GetUserId()!,
            cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("result")]
    public async Task<IActionResult> GetResult(
        int lessonId,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetLessonResultAsync(
            lessonId,
            User.GetUserId()!,
            cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
