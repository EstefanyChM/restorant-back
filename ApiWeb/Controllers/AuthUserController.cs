using AutoMapper;
using Business;
using Bussnies;
using DTO;
using IBussnies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RequestResponseModel;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[AllowAnonymous]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class AuthUserController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IAuthUserBusiness _authUserBusiness;

        public AuthUserController(IMapper mapper, IAuthUserBusiness authUserBusiness)
        {
            _mapper = mapper;
            _authUserBusiness = authUserBusiness;
        }


        [HttpPost("registrar/OnlineClient")] // api/cuentas/registrar/OnlineClient
        public async Task<ActionResult<AutenticacionResponse>> RegistrarOnlineClient(OnlineUserRegistrarDTO request)
        {
            var result = await _authUserBusiness.RegistrarOnlineClient(request);
            return (result);
        }

        //POLICY = ADMIN
        [HttpPost("registrar/UsuarioSistema")]
        public async Task<ActionResult<AutenticacionResponse>> RegistrarUsuarioSistema(UsuarioSistemaRegistrarDTO request)
        {
            var result = await _authUserBusiness.RegistrarUsuarioSistema(request);
            return (result);
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AutenticacionResponse>> Login([FromBody] LoginDTO request, [FromQuery] bool esPersonal = false)
        {
            var result = await _authUserBusiness.Login(request, esPersonal);
            return result;
        }




        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //
        public async Task<ActionResult<AutenticacionResponse>> RenovarToken()
        {
            var emailClaim = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "email");

            if (emailClaim == null) return BadRequest("Email claim not found.");

            var email = emailClaim.Value;

            return await _authUserBusiness.RenovarToken(email);
        }



        [HttpPost("RemoverAdmin")]
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO)
        {
            return await _authUserBusiness.RemoverAdmin(editarAdminDTO);
        }

        [HttpPost("asignar-rol")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignarRolDTO request)
        {
            return await _authUserBusiness.AsignarRol(request);
        }

    }
}
