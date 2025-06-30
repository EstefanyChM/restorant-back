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
using System.Security.Claims;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class PersonalEmpresaController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly IPersonalEmpresaBusiness _personalEmpresaBusiness;

        public PersonalEmpresaController(IMapper mapper, IPersonalEmpresaBusiness personalEmpresaBusiness)
        {
            _mapper = mapper;
            _personalEmpresaBusiness = personalEmpresaBusiness;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole = "";

        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA PERSONAL EMPRESA
        /// </summary>
        /// <returns>List-PersonalEmpresaResponse</returns>
        //[AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpGet]
        public async Task<ActionResult> Get() // llamar a la vista
        {
            return Ok(await _personalEmpresaBusiness.GetAll());
        }

        /// <summary>
        /// RETORNA: ROW PERSONAL EMPRESA - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-PersonalEmpresaResponse</returns>
        [HttpGet("{id}", Name = "ObtenerPersonalEmpresa")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            return Ok(await _personalEmpresaBusiness.GetById(id));
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsMozo")]

        [HttpGet]
        [Route("SystemUser/{idUsuario}")]
        //[Authorize(Roles = "Mozo")] aún no puedo autenticarme con futter pipippi
        public async Task<ActionResult<PersonalEmpresaResponse>> GetUsuarioSistema([FromRoute] int idUsuario)
        {
            return Ok(await _personalEmpresaBusiness.GetByIdUsuarioSistema(idUsuario));
        }

        /// <summary>
        /// INSERTA: ROW CLIENTE
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PersonalEmpresaCreateDTO request)
        {
            var result = await _personalEmpresaBusiness.Create(request);
            return CreatedAtRoute("ObtenerPersonalEmpresa", new { id = result.Id }, result);
        }


        /// <summary>
        /// ACTUALIZA: ROW PERSONAL EMPRESA
        /// </summary>
        /// <returns>List-PersonalEmpresaResponse</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] PersonalEmpresaRequest request)
        {
            return Ok(await _personalEmpresaBusiness.Update(request));
        }

        /// <summary>
        /// RETORNA:TABLA CARGO POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-PersonalEmpresaResponse</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPost("filter")]
        public ActionResult GetByFilter([FromBody] GenericFilterRequest request)
        {
            // GenericFilterResponse<PersonalEmpresaResponse> res = _PersonalEmpresaBussnies.GetByFilteDependingRole(request, userRole);

            // return Ok(res);
            throw new NotImplementedException();
        }
        /// <summary>
        /// ELIMINA: lógico - Column Estado
        /// </summary>
        /// <returns>List-PersonalEmpresaResponse</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            return Ok(await _personalEmpresaBusiness.LogicDelete(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _personalEmpresaBusiness.Delete(id));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPatch]
        /*
        [
          {
            "path": "/estado",
            "op": "replace",
            "value": "false"
          }
        ]
       */
        public async Task<ActionResult> Patch([FromRoute] int id, JsonPatchDocument<PersonalEmpresaRequest> patchDocument)
        {
            return Ok(await _personalEmpresaBusiness.Patch(id, patchDocument));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<PersonalEmpresaRequest> request)
        {
            List<PersonalEmpresaResponse> res = await _personalEmpresaBusiness.CreateMultiple(request);

            return Ok(res);
        }
        #endregion CRUD METHODS

    }
}
