namespace GraduationProject.Services;

public interface IUserDashboardService
{
    Task<Result<UserDashboardResponseDto>> GetDashboardAsync(string userId, CancellationToken cancellationToken = default);
}
