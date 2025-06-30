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

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[AllowAnonymous]
    public class ClienteController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IPersonaNaturalBussnies _PersonaBussnies;
        private readonly IMapper _mapper;
        private readonly IClienteBussnies _ClienteBussnies;
        private readonly IOnlineUserBusiness _onlineUserBusiness;

        public ClienteController(IMapper mapper, IClienteBussnies clienteBussnies, IOnlineUserBusiness onlineUserBusiness, IPersonaNaturalBussnies personaNaturalBussnies)
        {
            _mapper = mapper;
            _ClienteBussnies = clienteBussnies;
            _onlineUserBusiness = onlineUserBusiness;
            _PersonaBussnies = personaNaturalBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA CLIENTE
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get() // llamar a la vista
        {
            return Ok(await _ClienteBussnies.GetAll());
        }

        /// <summary>
        /// RETORNA: ROW CLIENTE - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [HttpGet("{id}", Name = "ObtenerCliente")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            return Ok(await _ClienteBussnies.GetById(id));
        }


        /// <summary>
        /// RETORNA: ROW CLIENTE - POR Id del OnlineUSer - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [HttpGet("OnlineUser/{id}", Name = "ObtenerClienteOnline")]
        public async Task<ActionResult> GetOnlineUser([FromRoute] int id)
        {
            return Ok(await _onlineUserBusiness.GetById(id));
        }

        /// <summary>
        /// INSERTA: ROW CLIENTE
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [HttpPost]
        [Route("natural")]
        public async Task<ActionResult> CreatePersonaNatural([FromBody] PersonaNaturalRequest request)
        {
            var result = await _ClienteBussnies.CreatePNat(request);
            return CreatedAtRoute("ObtenerCliente", new { id = result.Id }, result);
        }

        [HttpPost]
        [Route("juridica")]
        public async Task<ActionResult> CreatePersonaJuridica([FromBody] PersonaJuridicaRequest request)
        {
            var result = await _ClienteBussnies.CreatePJur(request);
            return CreatedAtRoute("ObtenerCliente", new { id = result.Id }, result);
        }


        /// <summary>
        /// ACTUALIZA: ROW CLIENTE
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ClienteRequest request)
        {
            return Ok(await _ClienteBussnies.Update(request));
        }

        /// <summary>
        /// RETORNA:TABLA CARGO POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            //userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            //string userRolee = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;


            GenericFilterResponse<ClienteResponse> res = await _ClienteBussnies.GetByFilter(request);

            return Ok(res);
            //throw                new NotImplementedException();
        }
        /// <summary>
        /// ELIMINA: lógico - Column Estado
        /// </summary>
        /// <returns>List-ClienteResponse</returns>
        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            return Ok(await _ClienteBussnies.LogicDelete(id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            return Ok(_ClienteBussnies.Delete(id));
        }

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
        public async Task<ActionResult> Patch([FromRoute] int id, JsonPatchDocument<ClienteRequest> patchDocument)
        {
            return Ok(await _ClienteBussnies.Patch(id, patchDocument));
        }


        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<ClienteRequest> request)
        {
            List<ClienteResponse> res = await _ClienteBussnies.CreateMultiple(request);

            return Ok(res);
        }
        #endregion CRUD METHODS

    }
}
