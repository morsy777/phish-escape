namespace GraduationProject.Services;

public interface IDashboardService
{
    Task<DashboardStatsResponse> GetDashboardStatsAsync();
}
