namespace GraduationProject.Persistence.EntitiesConfiguration;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder
            .HasIndex(x => new { x.QuestionId, x.AnswerText })
            .IsUnique();

        builder
            .Property(x => x.AnswerText)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.QuestionId);
    }
}
