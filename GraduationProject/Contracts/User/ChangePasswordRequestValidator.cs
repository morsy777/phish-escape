namespace GraduationProject.Contracts.User;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password should be at least 8 digits & should contains lowercase, nonalphanumeric and upper case.")
            .NotEqual(x => x.CurrentPassword)
            .WithMessage("New password cannot be same as the current password");
    }
}
