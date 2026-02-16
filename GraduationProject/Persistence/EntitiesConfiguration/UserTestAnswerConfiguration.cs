
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserTestAnswerConfiguration : IEntityTypeConfiguration<UserTestAnswer>
{
    public void Configure(EntityTypeBuilder<UserTestAnswer> builder)
    {
        // Attempt -> Cascade
        builder
            .HasOne(x => x.UserTestAttempt)
            .WithMany(a => a.UserTestAnswers)
            .HasForeignKey(x => x.UserTestAttemptId)
            .OnDelete(DeleteBehavior.Cascade);

        // TestItem -> NoAction (To Avoid Multiple Cascade Paths)
        builder
            .HasOne(x => x.TestItem)
            .WithMany(t => t.UserTestAnswers)
            .HasForeignKey(x => x.TestItemId)
            .OnDelete(DeleteBehavior.NoAction);

        // TestItemAnswer -> NoAction (To Avoid Multiple Cascade Paths)
        builder
            .HasOne(x => x.TestItemAnswer)
            .WithMany(a => a.UserTestAnswers)
            .HasForeignKey(x => x.TestItemAnswerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(x => x.AnsweredAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(x => x.UserTestAttemptId);

        builder.HasIndex(x => x.TestItemId);
    }
}
