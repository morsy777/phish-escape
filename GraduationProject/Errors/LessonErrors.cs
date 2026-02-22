namespace GraduationProject.Errors;

public static class LessonErrors
{
    public static readonly Error NotFound =
        new Error("Lesson.NotFound",
                  "Lesson not found",
                  StatusCodes.Status404NotFound);

    public static readonly Error DuplicateTitle =
        new Error("Lesson.DuplicateTitle",
                  "Lesson title already exists",
                  StatusCodes.Status409Conflict);

    public static readonly Error DuplicateOrderNumber =
        new Error("Lesson.DuplicateOrderNumber",
                  "Lesson order number already exists",
                  StatusCodes.Status409Conflict);
}