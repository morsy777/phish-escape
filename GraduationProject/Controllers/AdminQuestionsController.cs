namespace GraduationProject.Controllers;

[Route("admin/lessons/{lessonId}/questions")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class AdminQuestionsController(IAdminQuestionService adminQuestionService) : ControllerBase
{
    private readonly IAdminQuestionService _adminQuestionService = adminQuestionService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromRoute] int lessonId, CancellationToken cancellationToken)
    {
        var result = await _adminQuestionService.GetAllAsync(lessonId, cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{questionId}")]
    public async Task<IActionResult> Get([FromRoute] int lessonId, [FromRoute] int questionId, CancellationToken cancellationToken)
    {
        var result = await _adminQuestionService.GetAsync(lessonId, questionId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("")]
    public async Task<IActionResult> Create([FromRoute] int lessonId, [FromBody] CreateQuestionDto dto, CancellationToken cancellationToken)
    {
        var result = await _adminQuestionService.CreateAsync(lessonId, dto, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { lessonId, questionId = result.Value }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{questionId}")]
    public async Task<IActionResult> Update([FromRoute] int lessonId, [FromRoute] int questionId,
        [FromBody] UpdateQuestionDto dto, CancellationToken cancellationToken)
    {
        var result = await _adminQuestionService.UpdateAsync(lessonId, questionId, dto, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> Delete([FromRoute] int lessonId, [FromRoute] int questionId,
        CancellationToken cancellationToken)
    {
        var result = await _adminQuestionService.DeleteAsync(lessonId, questionId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
