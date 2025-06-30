using BDRiccosModel;
using RequestResponseModel;

namespace IBussnies
{
    public interface IVentaBussnies : ICRUDBussnies<VentaRequest, VentaResponse>
    {
        public Task<VentaResponse> GetVentaDetalladaById(int id);
        public Task<List<VentaResponse>> GetDeliveryPendiente(short idService);
        public Task<byte[]> GenerarBoletaPDF(int id);

        public Task<byte[]> GenerarPDFDetallesCocina(int id);



    }
}
