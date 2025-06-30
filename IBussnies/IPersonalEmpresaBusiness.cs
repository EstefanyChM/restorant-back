using CommonModel;
using DTO;
using RequestResponseModel;

namespace IBussnies
{
    public interface IPersonalEmpresaBusiness : ICRUDBussnies<PersonalEmpresaRequest, PersonalEmpresaResponse>
    {
        Task<PersonalEmpresaResponse> Create(PersonalEmpresaCreateDTO entity);

        Task<PersonalEmpresaResponse> GetByIdUsuarioSistema(int idUsuario);

        Task<PersonalEmpresaResponse> GetById(int id);


    }
}
