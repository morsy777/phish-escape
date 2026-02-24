namespace GraduationProject.Contracts.Question;

public sealed class UpdateAnswerDto
{
    public int AnswerId { get; set; }
    public string AnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
