using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IPedidoRepository : ICRUDRepository<Pedido>
	{
		Pedido ObtenerPedidoConNombreDelProducto(int id);

		Task<List<PedidoDelUsuarioResponse>> GetPedidosMozoFiltrado(int idUser, int? idProducto, DateTime? fechaInicio, DateTime? fechaFin);

		Task<List<Pedido>> GeAllCocina(bool PreparacionFinalizada);




	}
}
