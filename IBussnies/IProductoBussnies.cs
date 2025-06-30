using BDRiccosModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using RequestResponseModel;

namespace IBussnies
{
    public interface IProductoBussnies : ICRUDBussnies<ProductoRequest, ProductoResponse>
    {
        //Task<GenericFilterResponse<ProductoResponse>> GetByFilteDependingRole(GenericFilterRequest request, string userRole);

        public Task<List<ProductoResponse>> UpdateMultipleByCategory(int idCategory, bool newDisponibility);

        
    }
}
