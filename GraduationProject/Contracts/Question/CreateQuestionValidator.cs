
namespace GraduationProject.Contracts.Question;

public sealed class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.QuestionText)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.QuestionContent)
            .NotEmpty();

        RuleFor(x => x.QuestionType)
            .IsInEnum();

        RuleFor(x => x.Answers)
            .NotEmpty()
            .Must(a => a.Count >= 2)
            .WithMessage("Question must have at least two answers")
            .When(x => x.Answers != null);

        RuleFor(x => x.Answers)
            .Must(HaveExactlyOneCorrectAnswer)
            .WithMessage("Question can't have more than one correct answer")
            .When(x => x.Answers != null);

        RuleForEach(x => x.Answers)
            .ChildRules(answer =>
            {
                answer
                    .RuleFor(a => a.AnswerText)
                    .NotEmpty()
                    .MaximumLength(100);
            })
            .When(x => x.Answers != null);

        RuleFor(x => x.Answers)
            .NotNull()
            .Must(HaveUniqueAnswerText)
            .WithMessage("You can't add duplicate answers for the same question")
            .When(x => x.Answers != null);

    }

    private bool HaveExactlyOneCorrectAnswer(List<CreateAnswerDto> answers)
        => answers.Count(x => x.IsCorrect) == 1;

    private bool HaveUniqueAnswerText(List<CreateAnswerDto> answers)
        => answers.Select(x => x.AnswerText).Distinct().Count() == answers.Select(x => x.AnswerText).Count();

}
