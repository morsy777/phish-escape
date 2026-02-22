using GraduationProject.Entities.Enums;
namespace GraduationProject.Entities;

public sealed class Question
{
    public int QuestionId { get; set; }

    public int LessonId { get; set; } = default!;
    public Lesson Lesson { get; set; } = null!;

    public string QuestionText { get; set; } = string.Empty;
    public string QuestionContent { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Answer> Answers { get; set; } = [];
    public  ICollection<UserAnswer> UserAnswers { get; set; } = [];
}

