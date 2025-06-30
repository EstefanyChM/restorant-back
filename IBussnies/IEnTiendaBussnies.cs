using BDRiccosModel;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;

namespace IBussnies
{
	public interface IEnTiendaBussnies : ICRUDBussnies<EnTiendaRequest, EnTiendaResponse>
	{
		Task<List<PedidoMesaResponse>> GetPedidoMesa();
		Task<EnTiendaResponse> FinalizarPedido(int id);
		Task<List<EnTiendaResponse>> GetAllFinalizado();
		Task<EnTiendaResponse> CrearConIdUsuario(int idUser, EnTiendaRequest enTiendaRequest);


	}
}
