
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserStatsConfiguration : IEntityTypeConfiguration<UserStats>
{
    public void Configure(EntityTypeBuilder<UserStats> builder)
    {
        builder.HasKey(x => x.UserId);
    }
}
