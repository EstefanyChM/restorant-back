using BDRiccosModel;
using CommonModel;
using DTO;
using RequestResponseModel;

namespace IBussnies
{
    public interface IUsuarioSistemaBusiness : ICRUDBussnies<UsuariosSistemaRequest, UsuariosSistemaResponse>
    {
        Task<UsuariosSistemaResponse> GetById(int id);

        

    }
}
