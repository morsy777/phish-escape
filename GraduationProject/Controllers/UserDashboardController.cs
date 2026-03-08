namespace GraduationProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDashboardController(IUserDashboardService userDashboardService) : ControllerBase
{
    private readonly IUserDashboardService _userDashboardService = userDashboardService;

    [HttpGet("user-dashboard")]
    public async Task<IActionResult> GetUserDashboard(CancellationToken cancellationToken)
    {
        var result = await _userDashboardService.GetDashboardAsync(User.GetUserId()!, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
