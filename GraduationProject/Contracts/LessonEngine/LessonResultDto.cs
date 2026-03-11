namespace GraduationProject.Contracts.LessonEngine;

public sealed class LessonResultDto
{
    public int TotalQuestions { get; set; }

    public int CorrectAnswers { get; set; }

    public int WrongAnswers { get; set; }

    public int Score { get; set; }

    public bool Passed { get; set; }
}