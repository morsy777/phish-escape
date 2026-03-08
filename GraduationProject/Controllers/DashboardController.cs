namespace GraduationProject.Controllers;

[Route("api/Dashboard")]
[ApiController]
[Authorize]
public class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    private readonly IDashboardService _dashboardService = dashboardService;

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _dashboardService.GetDashboardStatsAsync();

        return Ok(result);
    }

}
