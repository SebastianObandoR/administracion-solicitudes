using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private const int TimeoutSeconds = 15;
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(_settings.Host) || string.IsNullOrWhiteSpace(_settings.FromAddress))
                throw new InvalidOperationException("La configuracion de correo (EmailSettings) esta incompleta.");

            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromAddress, _settings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8
            };

            message.To.Add(new MailAddress(to));

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.EnableSsl
            };

            if (!string.IsNullOrWhiteSpace(_settings.User))
                client.Credentials = new NetworkCredential(_settings.User, _settings.Password);

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(TimeoutSeconds));
            await client.SendMailAsync(message, cts.Token);
        }
    }
}