using AutoMapper;
using BDRiccosModel;
using Bussnies;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[AllowAnonymous]
    public class AsuntoMensajeController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IAsuntoMensajeBussnies _asuntoMensajeBussnies;
        public AsuntoMensajeController(IAsuntoMensajeBussnies asuntoMensajeBussnies)
        {
            _asuntoMensajeBussnies = asuntoMensajeBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _asuntoMensajeBussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _asuntoMensajeBussnies.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AsuntoMensajeRequest request)
        {
            return Ok(await _asuntoMensajeBussnies.Create(request));
        }

        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            GenericFilterResponse<AsuntoMensajeResponse> res = await _asuntoMensajeBussnies.GetByFilter(request);
            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<AsuntoMensajeRequest> request)
        {
            List<AsuntoMensajeResponse> res = await _asuntoMensajeBussnies.CreateMultiple(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] AsuntoMensajeRequest request)
        {
            return Ok(await _asuntoMensajeBussnies.Update(request));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_asuntoMensajeBussnies.Delete(id));
        }
        #endregion CRUD METHODS
    }
}
