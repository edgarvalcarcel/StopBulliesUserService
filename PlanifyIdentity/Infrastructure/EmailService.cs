using FluentEmail.Core;

namespace PlanifyIdentity.Infrastructure;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IFluentEmailFactory _fluentEmailFactory;

    public EmailService(ILogger<EmailService> logger, IFluentEmailFactory fluentEmailFactory)
    {
        _logger = logger;
        _fluentEmailFactory = fluentEmailFactory;
    }

    public async Task Send(EmailMessageModel emailMessage)
    {
        _logger.LogInformation("Sending email");
        await _fluentEmailFactory.Create().To(emailMessage.ToAddress)
            .Subject(emailMessage.Subject)
            .Body(emailMessage.Body, true) // true means this is an HTML format message
            .SendAsync();
    }
}

public interface IEmailService
{
    /// <summary>
    /// Send an email.
    /// </summary>
    /// <param name="emailMessage">Message object to be sent</param>
    Task Send(EmailMessageModel emailMessage);
}

public class EmailMessageModel
{
    public string ToAddress { get; set; }
    public string Subject { get; set; }
    public string? Body { get; set; }
    public string? AttachmentPath { get; set; }
    public EmailMessageModel(string toAddress, string subject, string? body = "")
    {
        ToAddress = toAddress;
        Subject = subject;
        Body = body;
    }
}
