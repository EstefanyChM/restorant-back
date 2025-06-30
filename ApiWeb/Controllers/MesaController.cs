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
    public class MesaController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMesaBussnies _mesaBussnies;
        private readonly IMapper _mapper;
        public MesaController(IMapper mapper, IMesaBussnies mesaBussnies)
        {
            _mapper = mapper;
            _mesaBussnies = mesaBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _mesaBussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _mesaBussnies.GetById(id, userRole));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Active")]
        public async Task<ActionResult> GetActive()
        {
            List<MesaResponse> mesasActivos = (await _mesaBussnies.GetAll()).Where(p => p.Estado == true).ToList();
           
            return Ok(mesasActivos);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MesaRequest request)
        {
            return Ok(await _mesaBussnies.Create(request));
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("MesasEnTienda")]
        public async Task<ActionResult> GetMesasEnTienda()
        {
            List<MesaResponse> mesas = await _mesaBussnies.GetMesasEnTienda();
            return Ok(mesas);
        }

        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            GenericFilterResponse<MesaResponse> res = await _mesaBussnies.GetByFilter(request);
            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<MesaRequest> request)
        {
            List<MesaResponse> res = await _mesaBussnies.CreateMultiple(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult>  Update([FromBody] MesaRequest request)
        {
            return Ok(await _mesaBussnies.Update(request));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_mesaBussnies.Delete(id));
        }
        #endregion CRUD METHODS
    }
}
