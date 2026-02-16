namespace GraduationProject.Persistence.EntitiesConfiguration;

public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Icon)
            .HasMaxLength(500);

        builder
            .Property(x => x.ConditionType)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.Description)
            .HasMaxLength(200);
    }
}
