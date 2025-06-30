using BDRiccosModel;
using RequestResponseModel;

namespace IRepository
{
    public interface IEnTiendaRepository : ICRUDRepository<EnTienda>
{
    GenericFilterResponse<EnTienda> GetByFilter(GenericFilterRequest request);

        Task<List<PedidoMesaResponse>> GetPedidoMesa();
        Task<List<EnTienda>> GetAllFinalizado();

}
}