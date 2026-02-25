namespace GraduationProject.Services;

public interface IUserManagementService
{
    Task<Result<IEnumerable<UserResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result> MakeAdminAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result> RemoveAdminAsync(string userId, CancellationToken cancellationToken = default);
}