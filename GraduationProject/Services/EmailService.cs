namespace GraduationProject.Services;

public class EmailService(IOptions<MailSettings> mailSettings) : IEmailSender
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // TODO: Define the message and the email that we use to send mails.
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail),
            Subject = subject
        };

        // TODO: Add The Destination mail 
        message.To.Add(MailboxAddress.Parse(email));

        // TODO: Add The email body 
        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        // TODO: add the body to the message (email)
        message.Body = builder.ToMessageBody();

        // TODO: use smtp to send the message
        using var smtp = new SmtpClient();

        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}
