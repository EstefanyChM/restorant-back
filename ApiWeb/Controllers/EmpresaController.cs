using AutoMapper;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
				Policy = "EsAdmin")]
	//[AllowAnonymous]
	public class EmpresaController : ControllerBase
	{


		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
		private readonly IMapper _mapper;
		private readonly IEmpresaBusiness _empresaBusiness;

		public EmpresaController(IMapper mapper, IEmpresaBusiness empresaBusiness)
		{
			_mapper = mapper;
			_empresaBusiness = empresaBusiness;

		}
		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

		string userRole = "";

		#region CRUD METHODS
		/// <summary>
		/// RETORNA: TABLA EMPRESA
		/// </summary>
		/// <returns>List-EmpresaResponse</returns>
		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult> Get() // llamar a la vista
		{
			return Ok(await _empresaBusiness.GetAll());
		}





		/// <summary>
		/// ACTUALIZA: ROW EMPRESA
		/// </summary>
		/// <returns>List-EmpresaResponse</returns>
		[HttpPut("{id:int}")]
		public async Task<ActionResult> Update([FromRoute] int id, [FromForm] EmpresaRequest request)
		{
			return Ok(await _empresaBusiness.Update(request));
		}

		#endregion CRUD METHODS*/

	}
}
