namespace GraduationProject.Services;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId, HttpRequest request);
    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    Task<Result> UploadProfileImage(string userId, IFormFile image);
    Task<Result<ProfileImageResponse>> GetProfileImage(string userId);
}
