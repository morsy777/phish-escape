namespace GraduationProject.Contracts.Question;

public sealed class UpdateQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;

    public string QuestionContent { get; set; } = string.Empty;

    public QuestionType QuestionType { get; set; }

    public List<UpdateAnswerDto> Answers { get; set; } = [];
}