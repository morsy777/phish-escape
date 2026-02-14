namespace GraduationProject.Controllers;

[Route("me")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("")]
    public async Task<IActionResult> Info()
    {
        var result = await _userService.GetProfileAsync(User.GetUserId()!, Request);

        return Ok(result.Value);
    }

    [HttpPut("info")]
    public async Task<IActionResult> Info([FromBody] UpdateProfileRequest request)
    {
        await _userService.UpdateProfileAsync(User.GetUserId()!, request);

        return NoContent(); 
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _userService.ChangePasswordAsync(User.GetUserId()!, request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("upload-profile-image")]
    public async Task<IActionResult> UploadProfileImage([FromForm] UploadProfileImageRequest request)
    {
        await _userService.UploadProfileImage(User.GetUserId()!, request.Image);
        return Created();
    }

    [HttpGet("get-profile-image")]
    public async Task<IActionResult> GetProfileImage()
    {
        var result = await _userService.GetProfileImage(User.GetUserId()!);
        return Ok(result.Value);
    }

}
