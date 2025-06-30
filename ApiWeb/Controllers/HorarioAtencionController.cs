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
    public class HorarioAtencionController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IHorarioAtencionBusiness _horarioAtencionBussnies;
        public HorarioAtencionController(IHorarioAtencionBusiness horarioAtencionBussnies)
        {
            _horarioAtencionBussnies = horarioAtencionBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _horarioAtencionBussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _horarioAtencionBussnies.GetById(id));
        }


        [HttpPut]
        public async Task<ActionResult> Update([FromBody] HorarioAtencionRequest request)
        {
            return Ok(await _horarioAtencionBussnies.Update(request));
        }


        #endregion CRUD METHODS
    }
}
