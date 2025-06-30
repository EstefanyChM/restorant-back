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
    public class DeliveryController : ControllerBase
    {
        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IDeliveryBussnies _deliveryBussnies;
        private readonly IMapper _mapper;
        public DeliveryController(IMapper mapper, IDeliveryBussnies deliveryBussnies)
        {
            _mapper = mapper;
            _deliveryBussnies = deliveryBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _deliveryBussnies.GetAll());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _deliveryBussnies.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Delivery delivery)
        {
            return Ok(await _deliveryBussnies.Create(delivery));
        }

        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest filter)
        {
            var res = await _deliveryBussnies.GetByFilter(filter);
            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<Delivery> deliveries)
        {
            var res = await _deliveryBussnies.CreateMultiple(deliveries);
            return Ok(res);
        }


        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Delivery delivery)
        {
            return Ok(await _deliveryBussnies.Update(delivery));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_deliveryBussnies.Delete(id));
        }
        #endregion CRUD METHODS
    }
}
