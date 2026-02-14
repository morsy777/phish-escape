
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class QuestionConfiguration : IEntityTypeConfiguration<UserBadge>
{
    public void Configure(EntityTypeBuilder<UserBadge> builder)
    {
        builder.HasKey(x => new { x.UserId, x.BadgeId });
    }
}
