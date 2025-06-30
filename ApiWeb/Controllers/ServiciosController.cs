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
	public class ServiciosController : ControllerBase
	{

		/*INYECCIÓN DE DEPENDENCIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
		private readonly IServiciosBussnies _serviciosBussnies;

		public ServiciosController(IServiciosBussnies serviciosBussnies)
		{
			_serviciosBussnies = serviciosBussnies;
		}
		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
		string userRole = "";
		#region CRUD METHODS
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			return Ok(await _serviciosBussnies.GetAll());
		}



		[HttpPost]
		public async Task<ActionResult> Create([FromBody] ServiciosRequest request)
		{
			return Ok(await _serviciosBussnies.Create(request));
		}



		[HttpPost("multiple")]
		public async Task<ActionResult> CreateMultiple([FromBody] List<ServiciosRequest> request)
		{
			List<ServiciosResponse> res = await _serviciosBussnies.CreateMultiple(request);
			return Ok(res);
		}


		[HttpPut]
		public async Task<ActionResult> Update([FromBody] ServiciosRequest request)
		{
			return Ok(await _serviciosBussnies.Update(request));
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok(_serviciosBussnies.Delete(id));
		}
		#endregion CRUD METHODS
	}
}
