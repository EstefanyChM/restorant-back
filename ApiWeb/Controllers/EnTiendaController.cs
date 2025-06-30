using AutoMapper;
using BDRiccosModel;
using Bussnies;
using IBussnies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdminOMozo")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    //[AllowAnonymous]
    public class EnTiendaController : ControllerBase
    {
        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IEnTiendaBussnies _enTiendaBussnies;
        private readonly IMapper _mapper;
        public EnTiendaController(IMapper mapper, IEnTiendaBussnies enTiendaBussnies)
        {
            _mapper = mapper;
            _enTiendaBussnies = enTiendaBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _enTiendaBussnies.GetAll());
        }


        [HttpGet]
        [Route("pedido-finalizado")]
        //[Authorize(Roles = "Vendedor")] 

        public async Task<ActionResult> GetFinalizado()
        {
            return Ok(await _enTiendaBussnies.GetAllFinalizado());
        }



        [HttpGet]
        [Route("pedido-mesa")]
        //[Authorize(Roles = "Mozo")]
        //[AllowAnonymous]

        public async Task<ActionResult> GetPedidosPorMesa()
        {
            return Ok(await _enTiendaBussnies.GetPedidoMesa());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _enTiendaBussnies.GetById(id));
        }


        [HttpPost]
        //[Authorize(Roles = "Mozo")] 
        public async Task<ActionResult> Create([FromBody] EnTiendaRequest request, [FromQuery] int idUser)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            //string email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            // var aa = User.Claims;
            return Ok(await _enTiendaBussnies.CrearConIdUsuario(idUser, request));
        }

        [HttpPatch("finalizar/{id}")]
        public async Task<ActionResult> FinalizarPedido(int id)
        {
            return Ok(await _enTiendaBussnies.FinalizarPedido(id));
        }

        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            GenericFilterResponse<EnTiendaResponse> res = await _enTiendaBussnies.GetByFilter(request);
            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<EnTiendaRequest> request)
        {
            List<EnTiendaResponse> res = await _enTiendaBussnies.CreateMultiple(request);
            return Ok(res);
        }





        [HttpPut]
        public async Task<ActionResult> Update([FromBody] EnTiendaRequest request)
        {
            return Ok(await _enTiendaBussnies.Update(request));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_enTiendaBussnies.Delete(id));
        }
        #endregion CRUD METHODS
    }
}
