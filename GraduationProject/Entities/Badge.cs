namespace GraduationProject.Entities;

public sealed class Badge
{
    public int BadgeId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string ConditionType { get; set; } = string.Empty;
    public int ConditionValue { get; set; } = default!;

    public ICollection<UserBadge> UserBadges { get; set; } = [];
}

