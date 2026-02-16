namespace GraduationProject.Persistence.EntitiesConfiguration;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder
            .Property(x => x.DifficultyLevel)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasIndex(x => x.OrderNumber)
            .IsUnique();

        builder
            .HasIndex(x => x.Title)
            .IsUnique();
    }
}

