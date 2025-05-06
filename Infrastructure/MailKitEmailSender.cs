using System;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using StopBulliesUserService.Domain.Entities;

namespace StopBulliesUserService.Infrastructure;
internal sealed class MailKitEmailSender : IEmailSender
{
    public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options, IOptions<MailDesign> optMailDesign,
        IOptions<AppSettings> optAppSettings)
    {
        Options = options.Value;
        OptMailDesign = optMailDesign.Value;
        OptAppSettings = optAppSettings.Value;
    }

    public MailKitEmailSenderOptions Options { get; set; }
    public MailDesign OptMailDesign { get; set; }
    public AppSettings OptAppSettings { get; set; }
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        string confirmLink = htmlMessage[htmlMessage.IndexOf("https")..htmlMessage.IndexOf("'>")];
        string customMessage = OptMailDesign.HtmlDesign.Replace("{{name}}", email.Trim().Split('@')[0])
        .Replace("{{confirm}}", confirmLink);
       
        return Execute(email, subject, customMessage);
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
