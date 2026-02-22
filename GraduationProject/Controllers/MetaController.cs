namespace GraduationProject.Controllers;

[Route("meta")]
[ApiController]
public class MetaController : ControllerBase
{
    [HttpGet("difficulty-levels")]
    public IActionResult GetDifficultyLevels()
    {
        var values = Enum.GetValues(typeof(DifficultyLevel))
            .Cast<DifficultyLevel>()
            .Select(e => new
            {
                id = (int)e,
                name = e.ToString()
            });

        return Ok(values);
    }

    [HttpGet("question-types")]
    public IActionResult GetQuestionTypes()
    {
        var values = Enum.GetValues(typeof(QuestionType))
            .Cast<QuestionType>()
            .Select(e => new
            {
                id = (int)e,
                name = e.ToString()
            });

        return Ok(values);
    }
}
