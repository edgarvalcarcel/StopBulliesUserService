using Microsoft.AspNetCore.Identity.UI.Services;

namespace PlanifyIdentity.Infrastructure;

public class EmailSender : IEmailSender
{
    private readonly IEmailService _emailService;

    public EmailSender(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        EmailMessageModel emailMessage = new(email,
        subject,
        htmlMessage);

        await _emailService.Send(emailMessage);
    }
}
