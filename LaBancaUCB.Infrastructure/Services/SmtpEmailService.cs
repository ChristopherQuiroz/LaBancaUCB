using System.Net;
using System.Net.Mail;
using LaBancaUCB.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LaBancaUCB.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly IConfiguration _config;

    public SmtpEmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnviarComprobanteAsync(string emailDestino, string asunto, string cuerpoHtml)
    {
        var smtpHost = _config["Smtp:Host"];
        var smtpPort = int.Parse(_config["Smtp:Port"] ?? "587");
        var smtpUser = _config["Smtp:User"];
        var smtpPass = _config["Smtp:Pass"];

        using var client = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpUser!, "La Banca UCB Notificaciones"),
            Subject = asunto,
            Body = cuerpoHtml,
            IsBodyHtml = true
        };
        mailMessage.To.Add(emailDestino);

        await client.SendMailAsync(mailMessage);
    }
}