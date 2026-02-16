namespace GraduationProject.Persistence.EntitiesConfiguration;

public class TestItemAnswerConfiguration : IEntityTypeConfiguration<TestItemAnswer>
{
    public void Configure(EntityTypeBuilder<TestItemAnswer> builder)
    {
        builder.HasKey(x => x.TestItemAnswerId);

        builder
            .HasOne(x => x.TestItem)
            .WithMany(t => t.TestItemAnswers)
            .HasForeignKey(x => x.TestItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(x => x.AnswerText)
            .IsRequired()
            .HasMaxLength(500);

        builder
            .HasIndex(x => new { x.TestItemId, x.AnswerText })
            .IsUnique();

        builder.HasIndex(x => x.TestItemId);
    }
}

