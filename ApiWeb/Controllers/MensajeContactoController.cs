using AutoMapper;
using Bussnies;
using DocumentFormat.OpenXml.Vml.Office;
using IBussnies;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;
using DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[AllowAnonymous]
    public class MensajeContactoController : ControllerBase
    {

        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly IMensajeContactoBussnies _mensajeContactoBussnies;

        public MensajeContactoController(IMapper mapper, IMensajeContactoBussnies mensajeContactoBussnies)
        {
            _mapper = mapper;
            _mensajeContactoBussnies = mensajeContactoBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole = "";

        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA MENSAJECONTACTO
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _mensajeContactoBussnies.GetAll());
        }


        /// <summary>
        /// RETORNA: ROW MENSAJECONTACTO - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpGet("{id}", Name = "ObtenerMensajeContacto")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            return Ok(await _mensajeContactoBussnies.GetById(id));
        }


        /// <summary>
        /// INSERTA: ROW MENSAJECONTACTO
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MensajeCrearDTO mensajeCrearDTO)
        {
            var result = await _mensajeContactoBussnies.Create(_mapper.Map<MensajeContactoRequest>(mensajeCrearDTO));
            return CreatedAtRoute("ObtenerMensajeContacto", new { id = result.Id }, result);
        }


        /// <summary>
        /// ACTUALIZA: ROW MENSAJECONTACTO
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] MensajeContactoRequest request)
        {
            return Ok(await _mensajeContactoBussnies.Update(request));
        }

        /// <summary>
        /// RETORNA:TABLA MENSAJECONTACTO POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            GenericFilterResponse<MensajeContactoResponse> res = await _mensajeContactoBussnies.GetByFilter(request);
            return Ok(res);
        }


        /// <summary>
        /// ELIMINA: lógico - Column Estado
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            return Ok(await _mensajeContactoBussnies.LogicDelete(id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            return Ok(_mensajeContactoBussnies.Delete(id));
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
        public async Task<ActionResult> Patch([FromRoute] int id, JsonPatchDocument<MensajeContactoRequest> patchDocument)
        {
            return Ok(await _mensajeContactoBussnies.Patch(id, patchDocument));
        }



        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<MensajeContactoRequest> request)
        {
            List<MensajeContactoResponse> res = await _mensajeContactoBussnies.CreateMultiple(request);

            return Ok(res);
        }

        #endregion CRUD METHODS
    }
}
