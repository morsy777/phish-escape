namespace GraduationProject.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials =
        new Error("User.InvalidCredentials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new Error("User.InvalidJwtToken", "Invalid Jwt Token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new Error("User.InvalidRefreshToken", "Invalid Refresh Token", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new Error("User.DuplicatedEmail", "Another User with the same Email already Exist", StatusCodes.Status409Conflict);
    
    public static readonly Error EmailNotConfirmed =
        new Error("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
        new Error("User.InvalidCode", "Invalid confirmation code", StatusCodes.Status401Unauthorized);
    
    public static readonly Error DuplicatedConfirmation =
        new Error("User.DuplicatedConfirmation", "The email already confrimed", StatusCodes.Status401Unauthorized);

}
