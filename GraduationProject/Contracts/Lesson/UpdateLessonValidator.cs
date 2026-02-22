namespace GraduationProject.Contracts.Lesson;

public sealed class UpdateLessonValidator : AbstractValidator<UpdateLessonDto>
{
    public UpdateLessonValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.OrderNumber)
            .GreaterThan(0);

        RuleFor(x => x.DifficultyLevel)
            .IsInEnum();

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}
