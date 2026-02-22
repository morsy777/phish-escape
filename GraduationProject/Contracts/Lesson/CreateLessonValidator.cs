namespace GraduationProject.Contracts.Lesson;

public sealed class CreateLessonValidator : AbstractValidator<CreateLessonDto>
{
    public CreateLessonValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.OrderNumber)
            .GreaterThan(0);

        RuleFor(x => x.DifficultyLevel)
            .IsInEnum();

        RuleFor(x => x.Description)
            .MaximumLength(2000);
    }
}