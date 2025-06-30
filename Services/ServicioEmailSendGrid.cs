using IServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BDRiccosModel;

namespace Services
{
    public class ServicioEmailSendGrid : IServicioEmailSendGrid
    {
        private readonly IConfiguration _configuration;

        public ServicioEmailSendGrid(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarMasivos(Promocion promocion, List<string> destinatarios)
        {
            var apiKey = _configuration["SENDGRID_API_KEY"];
            var email = _configuration["SENDGRID_FROM"];
            var nombre = _configuration["SENDGRID_NOMBRE"];
           // var idAsunto = 1; // Ajusta el ID del asunto si es necesario
        
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email, nombre);
            var subject = $"Promoción Especial: {promocion.Nombre}";
            
            // Contenido del mensaje en texto plano con fechas
            var plainTextContent = $@"
                ¡No te pierdas nuestra nueva promoción: {promocion.Nombre}!
                {promocion.Descripcion}
        
                Fechas de la promoción:
                Inicio: {promocion.FechaInicio.ToShortDateString()}
                Fin: {promocion.FechaFin.ToShortDateString()}
        
                ¡Aprovecha antes de que termine!
            ";
        
            // Contenido HTML que incluye las fechas
            var htmlContent = GenerarHtmlContentPromocion(promocion, nombre);
        
            foreach (var destinatario in destinatarios)
            {
                var to = new EmailAddress(destinatario);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
        
                // Manejar la respuesta si es necesario
                if (!response.IsSuccessStatusCode)
                {
                    // Registrar o manejar el error
                    Console.WriteLine($"Error al enviar el correo a {destinatario}: {response.StatusCode}");
                }
            }
        }

        private string ObtenerAsuntoNombre(int idAsunto)
        {
            return idAsunto switch
            {
                1 => "Disponibilidad",
                2 => "Políticas y Condiciones",
                3 => "Comentarios y Opiniones",
                4 => "Promociones y Ofertas",
                _ => "Información"
            };
        }

        private string GenerarHtmlContentPromocion(Promocion promocion, string remitente)
        {
            // Generar el contenido HTML del correo, incluyendo las     fechas  de inicio y fin
            return $@"
                <html>
                <body>
                    <h1>{promocion.Nombre}</h1>
                    <p>{promocion.Descripcion}</p>
                    <p><strong>Fecha de inicio:</strong>        {promocion.FechaInicio.ToShortDateString()}</p>
                    <p><strong>Fecha de fin:</strong>       {promocion.FechaFin.ToShortDateString()}</p>
                    <p>¡Aprovecha esta oferta antes de que termine!</p>
                    <img src='{promocion.UrlImagen}' alt='Imagen de la   promoción' style='width:100%;max-width:600px;'>
                    <br>
                    <p>Saludos,<br>{remitente}</p>
                </body>
                </html>";
        }
    }
}
