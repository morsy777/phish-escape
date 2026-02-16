
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserBadgeConfiguration : IEntityTypeConfiguration<UserBadge>
{
    public void Configure(EntityTypeBuilder<UserBadge> builder)
    {
        builder.HasKey(x => new { x.UserId, x.BadgeId });

        builder
            .Property(x => x.EarnedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.BadgeId);
    }
}
