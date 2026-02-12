namespace GraduationProject.Helpers;

public class EmailBodyBuilder
{
    private readonly IWebHostEnvironment _env;

    public EmailBodyBuilder(IWebHostEnvironment env)
    {
        _env = env;
    }

    /// <summary>
    /// Generates the email body from a template and replaces placeholders with values.
    /// </summary>
    /// <param name="templateName">The template file name without extension</param>
    /// <param name="templateModel">Dictionary of placeholders and their replacement values</param>
    /// <returns>Final email body as string</returns>
    public string GenerateEmailBody(string templateName, Dictionary<string, string> templateModel)
    {
        // Build the path dynamically relative to wwwroot
        var templatePath = Path.Combine(_env.WebRootPath, "Template", $"{templateName}.html");

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Email template not found at {templatePath}");

        string body;
        using (var reader = new StreamReader(templatePath))
        {
            body = reader.ReadToEnd();
        }

        // Replace placeholders in the template
        foreach (var kvp in templateModel)
        {
            body = body.Replace(kvp.Key, kvp.Value);
        }

        return body;
    }
}
