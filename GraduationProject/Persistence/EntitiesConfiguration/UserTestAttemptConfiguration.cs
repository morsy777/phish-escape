
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserTestAttemptConfiguration : IEntityTypeConfiguration<UserTestAttempt>
{
    public void Configure(EntityTypeBuilder<UserTestAttempt> builder)
    {
        builder
            .Property(x => x.Accuracy)
            .HasPrecision(5, 2);

        builder.HasIndex(x => x.UserId);
    }
}
