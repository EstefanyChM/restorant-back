using BDRiccosModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using RequestResponseModel;

namespace IBussnies
{
	public interface IClienteBussnies : ICRUDBussnies<ClienteRequest, ClienteResponse>
	{
		Task<ClienteResponse> CreatePJur (PersonaJuridicaRequest request);
		Task<ClienteResponse> CreatePNat (PersonaNaturalRequest request);


		/**************** BORRAR ***************/



		List<VCliente> ObtenerVistaCliente();
    GenericFilterResponse<VCliente> FiltradoDeVCliente(GenericFilterRequest request);

		GenericFilterResponse<VCliente> GetByFilterVCliente(GenericFilterRequest request);

		int Update(int id);

		List<VCliente>GetVClientActive();

		


	}
}