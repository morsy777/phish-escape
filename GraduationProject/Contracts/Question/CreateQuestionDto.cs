namespace GraduationProject.Contracts.Question;

public sealed class CreateQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;

    public string QuestionContent { get; set; } = string.Empty;

    public QuestionType QuestionType { get; set; }

    public List<CreateAnswerDto> Answers { get; set; } = [];
}