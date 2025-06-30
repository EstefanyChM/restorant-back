using BDRiccosModel;
using DTO;
using RequestResponseModel;

namespace IBussnies
{
    public interface IMensajeBusiness : ICRUDBussnies<MensajeContactoRequest, MensajeContactoResponse>
    {
        Task EnviarMensajeAsync(MensajeContactoRequest request);
    }
}
