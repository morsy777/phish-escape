
namespace GraduationProject.Persistence.EntitiesConfiguration;

public class TestItemConfiguration : IEntityTypeConfiguration<TestItem>
{
    public void Configure(EntityTypeBuilder<TestItem> builder)
    {
        builder
            .Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Content)
            .IsRequired();

        builder
            .HasIndex(x => x.OrderNumber)
            .IsUnique();
    }
}
