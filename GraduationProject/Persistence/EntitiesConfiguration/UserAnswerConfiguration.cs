namespace GraduationProject.Persistence.EntitiesConfiguration;

public class UserAnswerConfiguration
    : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasKey(x => x.UserAnswerId);

        builder
            .HasOne(x => x.Question)
            .WithMany(q => q.UserAnswers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Answer)
            .WithMany(a => a.UserAnswers)
            .HasForeignKey(x => x.AnswerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

