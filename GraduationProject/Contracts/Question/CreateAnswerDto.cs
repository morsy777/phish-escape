namespace GraduationProject.Contracts.Question;

public sealed class CreateAnswerDto
{
    public string AnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
