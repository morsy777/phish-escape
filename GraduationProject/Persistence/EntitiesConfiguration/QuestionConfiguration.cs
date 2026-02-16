
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasIndex(x => x.LessonId);

        builder
            .Property(x => x.QuestionText)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder
            .Property(x => x.ItemType)
            .IsRequired()
            .HasMaxLength(50);
    }
}
