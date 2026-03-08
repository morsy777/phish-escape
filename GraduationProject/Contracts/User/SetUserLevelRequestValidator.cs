namespace GraduationProject.Contracts.User;

public class SetUserLevelRequestValidator : AbstractValidator<SetUserLevelRequest>
{
    public SetUserLevelRequestValidator()
    {
        RuleFor(x => x.Level)
            .IsInEnum()
            .WithMessage("Invalid user level");
    }
}