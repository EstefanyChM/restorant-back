using AutoMapper;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using BDRiccosModel;
using Bussnies;
using Repository;
using Newtonsoft.Json;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[AllowAnonymous]
    public class PromocionController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly IPromocionBussnies _promocionBussnies;

        public PromocionController(IMapper mapper, IPromocionBussnies promocionBussnies)
        {
            _mapper = mapper;
            _promocionBussnies = promocionBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole = "";

        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA PROMOCION
        /// </summary>
        /// <returns>List-PromocionResponse</returns>

        [HttpGet]
        public async Task<ActionResult> Get() // llamar a la vista
        {
            return Ok(await _promocionBussnies.GetAll());
        }

        [HttpPost("{id:int}/enviar-masivos")]
        public async Task<IActionResult> EnviarMasivos(int id)
        {
            await _promocionBussnies.EnviarCorreosMasivos(id);

            return Ok(new { message = "Correos electrónicos enviados con     éxito." });
        }



        /// <summary>
        /// RETORNA: ROW PROMOCION - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-PromocionResponse</returns>
        [HttpGet("{id}", Name = "ObtenerPromocion")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            return Ok(await _promocionBussnies.GetById(id));
        }

        /// <summary>
        /// INSERTA: ROW PROMOCION
        /// </summary>
        /// <returns>List-PromocionResponse</returns>
        [HttpPost]
        //[HttpPost("crear-promocion")]
        public async Task<ActionResult> Create([FromBody] PromocionRequest promocionCrearDTO)
        {
            var result = await _promocionBussnies.Create(_mapper.Map<PromocionRequest>(promocionCrearDTO));
            return CreatedAtRoute("ObtenerPromocion", new { id = result.Id }, result);
        }

        [HttpPost("subir-imagen/{promocionId}")]
        public async Task<IActionResult> SubirImagen(int promocionId, IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("No se ha proporcionado una imagen válida.");
            }
            var result = await _promocionBussnies.SubirImagen(promocionId, foto);

            return CreatedAtRoute("ObtenerPromocion", new { id = result.Id }, result);
        }



        /// <summary>
        /// ACTUALIZA: ROW PROMOCION
        /// </summary>
        /// <returns>List-PromocionResponse</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] PromocionRequest request)
        {
            return Ok(await _promocionBussnies.Update(request));
        }

        /// <summary>
        /// RETORNA:TABLA PROMOCION POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-PromocionResponse</returns>
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            GenericFilterResponse<PromocionResponse> res = await _promocionBussnies.GetByFilter(request);

            return Ok(res);
        }

        /// <summary>
        /// ELIMINA: lógigo - Column Estado
        /// </summary>
        /// <returns>List-PromocionResponse</returns>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            var result = await _promocionBussnies.LogicDelete(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _promocionBussnies.Delete(id));
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
        public async Task<ActionResult> Patch([FromRoute] int id, JsonPatchDocument<PromocionRequest> patchDocument)
        {
            return Ok(await _promocionBussnies.Patch(id, patchDocument));
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<PromocionRequest> request)
        {
            List<PromocionResponse> res = await _promocionBussnies.CreateMultiple(request);
            return Ok(res);
        }
        #endregion CRUD METHODS
    }
}
