using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using PlanifyIdentity.Extensions;
namespace PlanifyIdentity.Infrastructure;

public sealed class EmailSender : IEmailSender
{
    private readonly IdentitySecrets secrets;
    public EmailSender(IdentitySecrets settings)
   => secrets = settings;
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        int port = secrets?.SMTPPort ?? 0;
        string smtp = secrets?.OutgoingServer;
        string from = secrets?.Username ?? "";
        string to = email;
        using var clientSmtp = new SmtpClient(smtp, port)
        {
            Host = "smtp.office365.com",
            //"domain.mail.protection.outlook.com",
            //"smtp.office365.com"
            UseDefaultCredentials = false,
            TargetName = "STARTTLS/smtp.office365.com",
            Credentials = new NetworkCredential(secrets?.Username, secrets?.Password),
            Port = port,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true,
        };
 
        using var mailMsg = new MailMessage(from,
                            to,
                            subject,
                            htmlMessage
                            );
        await clientSmtp.SendMailAsync(mailMsg);
    }
}
