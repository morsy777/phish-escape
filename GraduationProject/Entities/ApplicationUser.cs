namespace GraduationProject.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? profileImage { get; set; }

    public int CurrentStreak { get; set; } = default!;
    public int MaxStreak { get; set; } = default!;
    public string UserLevel { get; set; } = "Beginner";

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
    public ICollection<UserTestAttempt> UserTestAttempts { get; set; } = [];
    public ICollection<LessonScore> LessonScores { get; set; } = [];
    public ICollection<UserBadge> UserBadges { get; set; } = [];
    public UserStats? UserStats { get; set; }
}
