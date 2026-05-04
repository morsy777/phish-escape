namespace GraduationProject.Entities;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}