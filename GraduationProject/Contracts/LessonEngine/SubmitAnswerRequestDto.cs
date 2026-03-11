namespace GraduationProject.Contracts.LessonEngine;

public sealed class SubmitAnswerRequestDto
{
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}
