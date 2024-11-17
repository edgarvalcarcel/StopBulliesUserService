using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace PlanifyIdentity.Infrastructure;

internal class MailKitEmailSender : IEmailSender
{
    public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options)
    {
        Options = options.Value;
    }

    public MailKitEmailSenderOptions Options { get; set; }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Execute(email, subject, htmlMessage);
    }

    public async Task<bool> Execute(string to, string subject, string htmlMessage)
    {
        // create message
        using var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(Options.Sender_EMail)
        };

        if (!string.IsNullOrEmpty(Options.Sender_Name))
        {
            email.Sender.Name = Options.Sender_Name;
        }

        email.From.Add(email.Sender);
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

        // send email
        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
            await smtp.AuthenticateAsync(Options.Host_Username, Options.Host_Password);
            _ = await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        return await Task.FromResult(true);
    }
}
