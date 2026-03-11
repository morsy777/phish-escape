namespace GraduationProject.Contracts.LessonEngine;

public class SubmitAnswerValidator : AbstractValidator<SubmitAnswerRequestDto>
{
    public SubmitAnswerValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThan(0);

        RuleFor(x => x.AnswerId)
            .GreaterThan(0);
    }
}