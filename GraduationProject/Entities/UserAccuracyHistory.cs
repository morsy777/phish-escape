namespace GraduationProject.Entities;

public sealed class UserAccuracyHistory
{
    public int UserAccuracyHistoryId { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public double Accuracy { get; set; } = default!;
    public DateTime RecordDate { get; set; } = DateTime.UtcNow.Date;
}
