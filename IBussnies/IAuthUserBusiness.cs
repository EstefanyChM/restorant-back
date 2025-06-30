using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;

namespace IBussnies
{
    public interface IAuthUserBusiness
    {
        //LoginResponse Login(LoginRequest request);

        Task<ActionResult<AutenticacionResponse>> RegistrarOnlineClient(OnlineUserRegistrarDTO request);

        Task<ActionResult<AutenticacionResponse>> RegistrarUsuarioSistema(UsuarioSistemaRegistrarDTO request);

        Task<ActionResult<AutenticacionResponse>> Login(LoginDTO request, bool esPersonal);

        Task<ActionResult<AutenticacionResponse>> RenovarToken(string email);


        Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO);

        Task<ActionResult> AsignarRol(AsignarRolDTO request);


    }
}
