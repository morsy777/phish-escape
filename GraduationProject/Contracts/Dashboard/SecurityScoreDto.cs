namespace GraduationProject.Contracts.Dashboard;

public class SecurityScoreDto
{
    public int Score { get; set; }

    public double DetectionAccuracy { get; set; }

    public string Performance { get; set; } = string.Empty;
}