using Azure.Core;
using BDRiccosModel;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;

namespace IBussnies
{
	public interface IPedidoBussnies : ICRUDBussnies<PedidoRequest, PedidoResponse>
	{

		Task<PedidoResponse> UpdateWithIdUser(PedidoRequest request, int idUser);

		Task<List<PedidoDelUsuarioResponse>> GetPedidosMozoFiltrado(int idUser, int? idProducto, DateTime? fechaInicio, DateTime? fechaFin);

		Task<List<PedidoResponse>> GetAllCocina(bool PreparacionFinalizada);

		Task<DetallePedido> UpdateEstadoPreparacion(int idDetallePedido);

	}
}
