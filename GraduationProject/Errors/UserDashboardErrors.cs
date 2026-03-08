namespace GraduationProject.Errors;

public static class UserDashboardErrors
{
    public static readonly Error UserStatsNotFound =
        new Error(
            "UserDashboard.UserStatsNotFound",
            "There is no user statistics",
            StatusCodes.Status404NotFound
        );
}