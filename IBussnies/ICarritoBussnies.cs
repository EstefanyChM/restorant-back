using RequestResponseModel;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBussnies
{
    public interface ICarritoBussnies : ICRUDBussnies<CarritoRequest, CarritoResponse>
    {
        Task<CarritoResponse> Patch(int id, JsonPatchDocument<CarritoRequest> patchDocument);
    }
}

