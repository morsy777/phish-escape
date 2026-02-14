namespace GraduationProject.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : 
    IdentityDbContext<ApplicationUser>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Question> Questions { get; set; } = default!;
    public DbSet<Answer> Answers { get; set; } = default!;

    public DbSet<TestItem> TestItems { get; set; } = default!;
    public DbSet<TestItemAnswer> TestItemAnswers { get; set; } = default!;
    public DbSet<UserTestAttempt> UserTestAttempts { get; set; } = default!;
    public DbSet<UserTestAnswer> UserTestAnswers { get; set; } = default!;

    public DbSet<UserAnswer> UserAnswers { get; set; } = default!;
    public DbSet<LessonScore> LessonScores { get; set; } = default!;

    public DbSet<UserStats> UserStats { get; set; } = default!;
    public DbSet<UserAccuracyHistory> UserAccuracyHistory { get; set; } = default!;

    public DbSet<Badge> Badges { get; set; } = default!;
    public DbSet<UserBadge> UserBadges { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        return base.SaveChangesAsync(cancellationToken);
    }

}
