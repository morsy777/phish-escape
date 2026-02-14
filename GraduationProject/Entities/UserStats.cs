namespace GraduationProject.Entities;

public sealed class UserStats
{
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int SecurityScore { get; set; } = default!;
    public double GlobalRank { get; set; } = default!;
    public double DetectionAccuracy { get; set; } = default!;

    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}

