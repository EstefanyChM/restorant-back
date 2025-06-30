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
    public class PickupController : ControllerBase
    {
        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IPickupBussnies _pickupBussnies;
        private readonly IMapper _mapper;
        public PickupController(IMapper mapper, IPickupBussnies pickupBussnies)
        {
            _mapper = mapper;
            _pickupBussnies = pickupBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _pickupBussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _pickupBussnies.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Pickup pickup)
        {
            return Ok(await _pickupBussnies.Create(pickup));
        }

        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest filter)
        {
            var res = await _pickupBussnies.GetByFilter(filter);
            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<Pickup> pickups)
        {
            var res = await _pickupBussnies.CreateMultiple(pickups);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Pickup pickup)
        {
            return Ok(await _pickupBussnies.Update(pickup));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_pickupBussnies.Delete(id));
        }
        #endregion CRUD METHODS
    }
}
