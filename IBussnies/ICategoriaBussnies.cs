using BDRiccosModel;
using RequestResponseModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using Azure.Core;
using CommonModel;


namespace IBussnies
{
	public interface ICategoriaBussnies : ICRUDBussnies<CategoriaRequest, CategoriaResponse>
	{
		//PA PATCH

		Task<List<CategoriaResponse>> GetAllActive();
		Task<GenericFilterResponse<CategoriaResponse>> GetByFilteDependingRole(GenericFilterRequest request, string userRole);
		Task<CategoriaResponse> GetById(int id);
		Task<CategoriaResponse> GetByName(string nombreCategoria);


	}
}
