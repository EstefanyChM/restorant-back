using AutoMapper;
using Bussnies;
using IBussnies;
using IServices;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using DTO;
using Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BDRiccosModel;
using Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [AllowAnonymous]
    public class ProductoController : ControllerBase
    {
        /* INYECCIÓN DE DEPENDENCIAS */
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IProductoBussnies _productoBussnies;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        public ProductoController(IMapper mapper, IProductoBussnies productoBussnies, IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _productoBussnies = productoBussnies;
            _authorizationService = authorizationService;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        /*[HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _productoBussnies.GetAll());
        }


        /*[AllowAnonymous]
        [HttpGet]
        [Route("Active")]
        public async Task<ActionResult> GetActive()
        {
            List<ProductoResponse> productosActivos = (await _productoBussnies.GetAll()).Where(p => p.Estado == true).ToList();
           
            return Ok(productosActivos);
        }*/
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] bool? activo = null, bool? disponible = null, int? idCategoria = null)
        {
            var productos = await _productoBussnies.GetAll();

            if (activo.HasValue)
            {
                productos = productos.Where(p => p.Estado == activo.Value).ToList();

                if (disponible.HasValue)
                {
                    productos = productos.Where(p => p.Disponibilidad == disponible.Value).ToList();
                }
            }

            if (idCategoria.HasValue)
            {
                productos = productos.Where(p => p.IdCategoria == idCategoria.Value && p.Stock != 0).ToList();
            }
            return Ok(productos);
        }


        [HttpGet("{id}", Name = "ObtenerProducto")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            return Ok(await _productoBussnies.GetById(id));
        }






        [AllowAnonymous]
        [HttpGet]
        [Route("rangoPrecios")]
        public async Task<ActionResult> GetPriceRange()
        {
            var minPrecio = 0;
            var maxPrecio = 0;

            List<ProductoResponse> productos = await _productoBussnies.GetAll();

            //if (productos == null || !productos.Any()) 
            if (productos.Count != 0)
            {
                minPrecio = (int)Math.Ceiling(productos.Min(p => p.Precio)) - 1;
                maxPrecio = (int)Math.Ceiling(productos.Max(p => p.Precio)) + 1;
            }


            // Crear un array con el precio mínimo y máximo
            //decimal[] rangoPrecios = { minPrecio, maxPrecio };

            //return Ok(rangoPrecios);
            return Ok(new { minPrecio, maxPrecio });
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProductoRequest request)
        {
            var result = await _productoBussnies.Create(request);
            return CreatedAtRoute("ObtenerProducto", new { id = result.Id }, result);
            //return Ok(result);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] ProductoRequest request)
        {
            return Ok(await _productoBussnies.Update(request));
        }



        [AllowAnonymous]
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            //userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            //if (request.Filtros[2].Value == "false" && userRole != "Administrador" )

            var esAdmin = await _authorizationService.AuthorizeAsync(User, "esAdmin");

            if (request.Filtros[2].Value == "false" && !esAdmin.Succeeded)
            {
                return StatusCode(403, new { message = "No tienes los permisos necesarios para realizar esta acción." });
            }
            else
            {
                GenericFilterResponse<ProductoResponse> res = await _productoBussnies.GetByFilter(request);
                return Ok(res);
            }
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<ProductoRequest> request)
        {
            List<ProductoResponse> res = await _productoBussnies.CreateMultiple(request);
            return Ok(res);
        }

        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            return Ok(await _productoBussnies.LogicDelete(id));
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            return Ok(_productoBussnies.Delete(id));
        }


        #endregion CRUD METHODS
    }
}
