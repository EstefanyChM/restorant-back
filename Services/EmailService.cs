using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using RequestResponseModel;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(MensajeContactoRequest model)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(model.RemiteNombre, smtpSettings["Email"])); // Remitente
            emailMessage.To.Add(new MailboxAddress("", smtpSettings["Email"])); // Destinatario, usualmente tu propio correo
            emailMessage.Subject = "Nuevo mensaje de contacto";
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Nombre: {model.RemiteNombre}\nEmail: {model.RemiteEmail}\nMensaje: {model.Mensaje}"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings["Host"], int.Parse(smtpSettings["Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpSettings["Email"], smtpSettings["Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
