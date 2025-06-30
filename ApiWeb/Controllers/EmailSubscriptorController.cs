using AutoMapper;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Net;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Bussnies;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    //[AllowAnonymous]
    public class EmailSuscriptorController : ControllerBase
    {
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly IEmailSuscriptorBusiness _emailSuscriptorBusiness;

        public EmailSuscriptorController(IMapper mapper, IEmailSuscriptorBusiness emailSuscriptorBusiness)
        {
            _mapper = mapper;
            _emailSuscriptorBusiness = emailSuscriptorBusiness;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole = "";

        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA EMAIL SUSCRIPTOR
        /// </summary>
        /// <returns>List-EmailSuscriptorResponse</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get() // llamar a la vista
        {
            return Ok(await _emailSuscriptorBusiness.GetAll());
        }

        /// <summary>
        /// RETORNA: ROW EMAIL SUSCRIPTOR - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>EmailSuscriptorResponse</returns>
        [HttpGet("{id}", Name = "ObtenerEmailSuscriptor")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            return Ok(await _emailSuscriptorBusiness.GetById(id));
        }

        /// <summary>
        /// INSERTA: ROW EMAIL SUSCRIPTOR
        /// </summary>
        /// <returns>EmailSuscriptorResponse</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EmailSubscriptorDTO request)
        {

            var result = await _emailSuscriptorBusiness.Create(_mapper.Map<EmailSuscriptorRequest>(request));
            return CreatedAtRoute("ObtenerEmailSuscriptor", new { id = result.Id }, result);
        }



        /// <summary>
        /// ACTUALIZA: ROW EMAIL SUSCRIPTOR
        /// </summary>
        /// <returns>EmailSuscriptorResponse</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] EmailSuscriptorRequest request)
        {
            return Ok(await _emailSuscriptorBusiness.Update(request));
        }

        /// <summary>
        /// RETORNA: TABLA EMAIL SUSCRIPTOR POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-EmailSuscriptorResponse</returns>
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            GenericFilterResponse<EmailSuscriptorResponse> res = await _emailSuscriptorBusiness.GetByFilter(request);

            return Ok(res);
        }

        /// <summary>
        /// ELIMINA: lógico - Column Estado
        /// </summary>
        /// <returns>EmailSuscriptorResponse</returns>
        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            return Ok(await _emailSuscriptorBusiness.LogicDelete(id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            return Ok(_emailSuscriptorBusiness.Delete(id));
        }

        [HttpPatch]
        public async Task<ActionResult> Patch([FromRoute] int id, JsonPatchDocument<EmailSuscriptorRequest> patchDocument)
        {
            return Ok(await _emailSuscriptorBusiness.Patch(id, patchDocument));
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<EmailSuscriptorRequest> request)
        {
            List<EmailSuscriptorResponse> res = await _emailSuscriptorBusiness.CreateMultiple(request);

            return Ok(res);
        }
        #endregion CRUD METHODS
    }
}
