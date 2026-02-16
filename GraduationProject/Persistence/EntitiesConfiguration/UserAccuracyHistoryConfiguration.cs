
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserAccuracyHistoryConfiguration : IEntityTypeConfiguration<UserAccuracyHistory>
{
    public void Configure(EntityTypeBuilder<UserAccuracyHistory> builder)
    {
        builder
            .Property(x => x.Accuracy)
            .HasPrecision(5, 2);

        builder
            .HasIndex(x => new { x.UserId, x.RecordDate })
            .IsUnique();
    }
}
