namespace GraduationProject.Contracts.UserManagement;

public sealed class UserResponseDto
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsAdmin { get; set; }
}
