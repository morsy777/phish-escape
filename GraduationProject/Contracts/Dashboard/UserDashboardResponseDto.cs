namespace GraduationProject.Contracts.Dashboard;

public class UserDashboardResponseDto
{
    public SecurityScoreDto SecurityScore { get; set; } = default!;

    public SimulationStatsDto Simulations { get; set; } = default!;

    public RankDto Rank { get; set; } = default!;

    public StreakDto Streak { get; set; } = default!;

    public List<ActiveLessonDto> ActiveLessons { get; set; } = [];
}