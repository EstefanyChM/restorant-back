using BDRiccosModel;
using RequestResponseModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using Azure.Core;
using Microsoft.AspNetCore.Http;

namespace IBussnies
{
	public interface IPromocionBussnies : ICRUDBussnies<PromocionRequest, PromocionResponse>
	{
		Task EnviarCorreosMasivos(int idPromocion);
		Task<PromocionResponse> SubirImagen( int promocionId, IFormFile foto);
	}
}
