using BDRiccosModel;
using RequestResponseModel;

namespace IBussnies
{
    public interface IMesaBussnies : ICRUDBussnies<MesaRequest, MesaResponse>
    {
        Task<List<MesaResponse>> GetMesasEnTienda();
        Task<MesaResponse> GetById(int id, string userRole);

    }
}
