
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserStatsConfiguration : IEntityTypeConfiguration<UserStats>
{
    public void Configure(EntityTypeBuilder<UserStats> builder)
    {
        builder.HasKey(x => x.UserId);

        builder
            .Property(x => x.DetectionAccuracy)
            .HasPrecision(5, 2)
            .HasDefaultValue(0);

        builder
            .Property(x => x.GlobalRank)
            .HasPrecision(10, 2);

        builder
            .Property(x => x.CalculatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
