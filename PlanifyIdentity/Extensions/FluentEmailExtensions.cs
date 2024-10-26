using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using System.Net.Mail;

namespace PlanifyIdentity.Extensions;

public static class FluentEmailExtensions
{
    public static void AddFluentEmail(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        IConfigurationSection emailSettings = configuration.GetSection("EmailSettings");
        string? defaultFromEmail = emailSettings["DefaultFromEmail"];
        string? host = emailSettings["Host"];
        int port = emailSettings.GetValue<int>("Port");
        services.AddFluentEmail(defaultFromEmail);
        services.AddSingleton<ISender>(x => new SmtpSender(new SmtpClient(host, port))); // I'm using dev mode using 'smtp4dev' hence i'm only using host and port
    }
}
