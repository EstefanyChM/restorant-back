using AutoMapper;
using Bussnies;
using IBussnies;
using IServices;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using DTO;
using Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Azure.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[AllowAnonymous]
    public class CarritoController : ControllerBase
    {
        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly ICarritoBussnies _carritoBussnies;
        private readonly IMapper _mapper;

        public CarritoController(IMapper mapper)
        {
            _mapper = mapper;
            _carritoBussnies = new CarritoBussnies(mapper);
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS

        [HttpGet]
        public IActionResult Get() // Obtener todos los carritos
        {
            return Ok(_carritoBussnies.GetAll());
        }


        [HttpGet("{id}", Name = "ObtenerCarrito")]
        public IActionResult Get(int id) // Obtener un carrito por ID
        {
            return Ok(_carritoBussnies.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarritoRequest request) // Crear un nuevo carrito
        {
            return Ok(_carritoBussnies.Create(request));
        }


        /*[HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CarritoRequest request) // Actualizar un carrito existente
        {
            
            return Ok(_carritoBussnies.Update(id, request));
            
        }*/

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) // Eliminar un carrito por ID
        {
            return Ok(_carritoBussnies.Delete(id));
        }

        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request) // Filtrar carritos
        {
            GenericFilterResponse<CarritoResponse> res = await _carritoBussnies.GetByFilter(request);
            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<CarritoRequest> request) // Crear múltiples carritos
        {
            List<CarritoResponse> res = await _carritoBussnies.CreateMultiple(request);
            return Ok(res);
        }

        #endregion CRUD METHODS
    }
}
