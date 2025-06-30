using ApiWeb.Midleware;
using AutoMapper;
using BDRiccosModel;
using DTO;
using Bussnies;
using CommonModel;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;

namespace ApiWeb.Controllers
{
	/// <summary>
	/// controller para la tabla/vista persona
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class PersonaController : ControllerBase
	{

		/*INYECCIÓN DE DEPENDECIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		private readonly IPersonaNaturalBussnies _PersonaNaturalBussnies;
		private readonly IPersonaJuridicaBussnies _PersonaJuridicaBussnies;
		private readonly IMapper _mapper;
		private readonly IHelperHttpContext _helperHttpContext = null;
		/// <summary>
		/// coonstructor
		/// </summary>
		/// <param name="mapper"></param>
		public PersonaController(IMapper mapper, IPersonaNaturalBussnies personaNaturalBussnies, IPersonaJuridicaBussnies personaJuridicaBussnies)
		{
			_mapper = mapper;
			_PersonaNaturalBussnies = personaNaturalBussnies;
			_PersonaJuridicaBussnies = personaJuridicaBussnies;

			//_helperHttpContext = new HelperHttpContext();
		}

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		string userRole = "";

		/// <summary>
		/// retorna los datos de una persona en base al DNI
		/// </summary>
		/// <returns></returns>
		[HttpGet("{tipDocumento}/{nroDocumento}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<VPersona>))]
		[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(GenericResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(GenericResponse))]
		public IActionResult GetWithDocument(int tipDocumento, string nroDocumento)
		{
			if (tipDocumento != 2)
			{
				PersonaNaturalResponse persona = _PersonaNaturalBussnies.GetByTipoNroDocumento(tipDocumento, nroDocumento);
				return Ok(persona);
			}

			else
			{
				PersonaJuridicaResponse persona = _PersonaJuridicaBussnies.GetByTipoNroDocumento(tipDocumento, nroDocumento);
				return Ok(persona);
			}

		}

	}
}
