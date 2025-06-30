using BDRiccosModel;
using RequestResponseModel;
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendEmailAsync(MensajeContactoRequest model);
}