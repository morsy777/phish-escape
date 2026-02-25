namespace GraduationProject.Errors;

public static class UserManagementErrors
{
    public static readonly Error UserNotFound =
        new("User.NotFound",
            "User not found",
            StatusCodes.Status404NotFound);

    public static readonly Error AlreadyAdmin =
        new("User.AlreadyAdmin",
            "User is already admin",
            StatusCodes.Status400BadRequest);

    public static readonly Error NotAdmin =
        new("User.NotAdmin",
            "User is not admin",
            StatusCodes.Status400BadRequest);
}
