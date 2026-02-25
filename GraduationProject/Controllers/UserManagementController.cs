namespace GraduationProject.Controllers;

[Route("api/admin/users")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class UserManagementController(IUserManagementService userManagementService) : ControllerBase
{
    private readonly IUserManagementService _userManagementService = userManagementService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userManagementService.GetAllAsync(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{userId}/make-admin")]
    public async Task<IActionResult> MakeAdmin([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var result = await _userManagementService.MakeAdminAsync(userId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{userId}/remove-admin")]
    public async Task<IActionResult> RemoveAdmin([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var result = await _userManagementService.RemoveAdminAsync(userId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
