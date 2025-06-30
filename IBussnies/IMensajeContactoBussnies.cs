using BDRiccosModel;
using DTO;
using RequestResponseModel;

namespace IBussnies
{
    public interface IMensajeContactoBussnies : ICRUDBussnies<MensajeContactoRequest, MensajeContactoResponse>
    {
        Task<List<MensajeContacto>> GetAllConDetalles();
        /**********************************/

    }
}
