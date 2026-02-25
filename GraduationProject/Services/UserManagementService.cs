namespace GraduationProject.Services;

public class UserManagementService(UserManager<ApplicationUser> userManager) : IUserManagementService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly string _admin = "Admin";

    public async Task<Result<IEnumerable<UserResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = _userManager.Users.ToList();

        var result = new List<UserResponseDto>();

        foreach(var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            result.Add(new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email!,
                IsAdmin = roles.Contains(_admin)
            });
        }

        return Result.Success<IEnumerable<UserResponseDto>>(result);
    }

    public async Task<Result> MakeAdminAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserManagementErrors.UserNotFound);

        if(await _userManager.IsInRoleAsync(user, _admin))
            return Result.Failure(UserManagementErrors.AlreadyAdmin);

        await _userManager.AddToRoleAsync(user, _admin);

        return Result.Success();
    }

    public async Task<Result> RemoveAdminAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserManagementErrors.UserNotFound);

        if (!await _userManager.IsInRoleAsync(user, _admin))
            return Result.Failure(UserManagementErrors.NotAdmin);

        await _userManager.RemoveFromRoleAsync(user, _admin);

        return Result.Success();
    }
}
