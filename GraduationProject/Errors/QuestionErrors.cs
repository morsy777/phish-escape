namespace GraduationProject.Errors;

public static class QuestionErrors
{
    public static readonly Error NotFound =
        new Error("Question.NotFound",
                  "Question not found",
                  StatusCodes.Status404NotFound);

    public static readonly Error DuplicateQuestionContent =
        new Error("Question.DuplicateContent",
                  "Question content already exists in this lesson",
                  StatusCodes.Status409Conflict);

    public static readonly Error InvalidAnswers =
        new Error("Question.InvalidAnswers",
                  "Question must have at least two answers and exactly one correct answer",
                  StatusCodes.Status400BadRequest);

    public static readonly Error InvalidAnswerReference =
        new Error("Question.InvalidAnswerReference",
              "One or more answer IDs don't belong to this question",
              StatusCodes.Status400BadRequest);

    public static readonly Error LessonNotFound =
        new Error("Question.LessonNotFound",
                  "Lesson not found",
                  StatusCodes.Status404NotFound);
}