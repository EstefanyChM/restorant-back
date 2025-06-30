using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IVentaRepository : ICRUDRepository<Venta>
    {
        public Task<VistaVenta> GetVistaVentaById(int id);
        public Task<Venta> GetVentaBoletaFactura(int id);

        public Task<Venta> GetDetallesCocina(int id);

        public Task<List<VistaDetallePedido>> GetListaVistaDetallePedido(int id);

        public Task<List<Venta>> GetListaVentaOnlinePagada(int IdService);



        
    }
}
