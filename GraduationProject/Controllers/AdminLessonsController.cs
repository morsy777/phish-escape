namespace GraduationProject.Controllers;

[Route("api/admin/lessons")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class AdminLessonsController(IAdminLessonService adminLessonService) : ControllerBase
{
    private readonly IAdminLessonService _adminLessonService = adminLessonService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _adminLessonService.GetAllAsync(cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _adminLessonService.GetAsync(id, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateLessonDto dto, CancellationToken cancellationToken)
    {
        var result = await _adminLessonService.CreateAsync(dto, cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value) 
            : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateLessonDto dto, CancellationToken cancellationToken)
    {
        var result = await _adminLessonService.UpdateAsync(id, dto, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _adminLessonService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
