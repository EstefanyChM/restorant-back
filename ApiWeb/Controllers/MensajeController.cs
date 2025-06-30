using AutoMapper;
using Bussnies;
using DocumentFormat.OpenXml.Vml.Office;
using IBussnies;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml;



namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[AllowAnonymous]
	public class MensajeController : ControllerBase
	{
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
		private readonly IMensajeBusiness _mensajeBusiness;

		public MensajeController(IMapper mapper, IMensajeBusiness  mensajeBusiness)
        {
            _mapper = mapper;
			_mensajeBusiness = mensajeBusiness;
		}
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole ="";

        /// <summary>
        /// INSERTA: ROW MENSAJECONTACTO
        /// </summary>
        /// <returns>List-MensajeContactoResponse</returns>
        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarMensaje([FromBody] MensajeContactoRequest request)
        {
            await _mensajeBusiness.EnviarMensajeAsync(request);
            return Ok("Mensaje enviado exitosamente.");
            /*try
            {
                await _mensajeBusiness.EnviarMensajeAsync(request);
                return Ok("Mensaje enviado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al enviar el mensaje: {ex.Message}");
            }*/
        }
        

	}
}
