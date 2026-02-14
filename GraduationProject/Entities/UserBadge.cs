namespace GraduationProject.Entities;

public sealed class UserBadge
{
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int BadgeId { get; set; } = default!;
    public Badge Badge { get; set; } = null!;

    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
}
