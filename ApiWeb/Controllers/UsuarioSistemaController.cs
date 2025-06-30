using AutoMapper;
using BDRiccosModel;
using Bussnies;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Firebase.Auth;
using Firebase.Storage;
using System.Data.SqlClient;
using System.Data;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using CommonModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

    //[AllowAnonymous]
    public class UsuarioSistemaController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly IUsuarioSistemaBusiness _usuarioSistemaBusiness;

        public UsuarioSistemaController(IMapper mapper, IUsuarioSistemaBusiness usuarioSistemaBusiness)
        {
            _mapper = mapper;
            _usuarioSistemaBusiness = usuarioSistemaBusiness;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole = "";

        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA USUARIO SISTEMA
        /// </summary>
        /// <returns>List-UsuarioSistemaResponse</returns>
        //[AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpGet]
        public async Task<ActionResult> Get() // llamar a la vista
        {
            return Ok(await _usuarioSistemaBusiness.GetAll());
        }

        /// <summary>
        /// RETORNA: ROW USUARIO SISTEMA - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-UsuarioSistemaResponse</returns>
        [HttpGet("{id}", Name = "ObtenerUsuarioSistema")]
        //[Authorize(Roles = "Mozo")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            //userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            return Ok(await _usuarioSistemaBusiness.GetById(id));
        }


        /// <summary>
        /// ACTUALIZA: ROW USUARIO SISTEMA
        /// </summary>
        /// <returns>List-UsuarioSistemaResponse</returns>
        //[Authorize(Roles = "Administrador,Vendedor,Mozo,Cocina")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UsuariosSistemaRequest request)
        {
            return Ok(await _usuarioSistemaBusiness.Update(request));
        }

        /// <summary>
        /// RETORNA:TABLA CARGO POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-UsuarioSistemaResponse</returns>
        [HttpPost("filter")]
        public ActionResult GetByFilter([FromBody] GenericFilterRequest request)
        {
            // GenericFilterResponse<UsuarioSistemaResponse> res = _usuarioSistemaBusiness.GetByFilteDependingRole(request, userRole);

            // return Ok(res);
            throw new NotImplementedException();
        }

        /// <summary>
        /// ELIMINA:  Column Estado
        /// </summary>
        /// <returns>List-UsuarioSistemaResponse</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _usuarioSistemaBusiness.Delete(id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<UsuariosSistemaRequest> request)
        {
            List<UsuariosSistemaResponse> res = await _usuarioSistemaBusiness.CreateMultiple(request);

            return Ok(res);
        }
        #endregion CRUD METHODS

    }
}
