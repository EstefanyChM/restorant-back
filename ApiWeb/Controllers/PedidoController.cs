using AutoMapper;
using BDRiccosModel;
using Bussnies;
using IBussnies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[AllowAnonymous]
    public class PedidoController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IPedidoBussnies _pedidoBussnies;
        private readonly IMapper _mapper;

        public PedidoController(IMapper mapper, IPedidoBussnies pedidoBussnies)
        {
            _mapper = mapper;
            _pedidoBussnies = pedidoBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _pedidoBussnies.GetAll());
        }

        [HttpGet]
        [Route("cocina")]
        public async Task<ActionResult> GetCocina([FromQuery] bool PreparacionFinalizada)
        {
            var result = await _pedidoBussnies.GetAllCocina(PreparacionFinalizada);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _pedidoBussnies.GetById(id));
        }


        /*[HttpGet("DetallePedido/{idUser}")]
        public async Task<ActionResult> GetDetallePedidoPorUsuario(int idUser)
        {
            return Ok(await _pedidoBussnies.GetDetallePedidoPorUsuario(idUser));
        }*/


        [HttpGet("pedidos-filtrado")]
        public async Task<ActionResult> GetPedidosMozoFiltrado(int idUser, int? idProducto, DateTime? fechaInicio, DateTime? fechaFin)
        {
            List<PedidoDelUsuarioResponse> pedidos = await _pedidoBussnies.GetPedidosMozoFiltrado(idUser, idProducto, fechaInicio, fechaFin);
            return Ok(pedidos);
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PedidoRequest request, int idUser)
        {
            return Ok(await _pedidoBussnies.Create(request));
        }


        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            GenericFilterResponse<PedidoResponse> res = await _pedidoBussnies.GetByFilter(request);

            return Ok(res);
        }


        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<PedidoRequest> request)
        {
            List<PedidoResponse> res = await _pedidoBussnies.CreateMultiple(request);

            return Ok(res);
        }



        [HttpPut]

        public async Task<ActionResult> Update([FromBody] PedidoRequest request, int idUser)
        {
            return Ok(await _pedidoBussnies.UpdateWithIdUser(request, idUser));
        }


        [HttpPut("dp-preparacion/{idDetallePedido}")]
        public async Task<ActionResult> Update([FromRoute] int idDetallePedido)
        {
            return Ok(await _pedidoBussnies.UpdateEstadoPreparacion(idDetallePedido));
        }



        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            return Ok(await _pedidoBussnies.LogicDelete(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_pedidoBussnies.Delete(id));
        }
        #endregion CRUD METHODS
    }
}
