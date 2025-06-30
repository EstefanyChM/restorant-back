using AutoMapper;
using Bussnies;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Infrastructure;
using RequestResponseModel;
using System.Net;


using QuestPDF.Fluent;
using System.IO;
using UtilPdf;
using BDRiccosModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador,Vendedor")]
    public class VentaController : ControllerBase
    {
        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly IVentaBussnies _ventaBussnies;

        public VentaController(IMapper mapper, IVentaBussnies ventaBussnies)
        {
            _mapper = mapper;
            _ventaBussnies = ventaBussnies;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        string userRole = "";
        #region CRUD METHODS

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _ventaBussnies.GetAll());
        }

        [HttpGet]
        [Route("VentaOnlinePagada/{idService}")]



        public async Task<ActionResult> GetDeliveryPendiente(short idService)
        {
            return Ok(await _ventaBussnies.GetDeliveryPendiente(idService));
        }


        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    return Ok(_ventaBussnies.GetById(id));
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _ventaBussnies.GetById(id));
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VentaRequest request)
        {
            return Ok(await _ventaBussnies.Create(request));
        }


        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            GenericFilterResponse<VentaResponse> res = await _ventaBussnies.GetByFilter(request);

            return Ok(res);
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<VentaRequest> request)
        {
            List<VentaResponse> res = await _ventaBussnies.CreateMultiple(request);

            return Ok(res);
        }



        [HttpPut]
        public async Task<ActionResult> Update([FromBody] VentaRequest request)
        {
            return Ok(await _ventaBussnies.Update(request));
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_ventaBussnies.Delete(id));
        }
        #endregion CRUD METHODS



        #region GENERATE DOCUMENTS


        [HttpGet]
        [Route("generate-comprobante/{id}")]
        public async Task<IActionResult> GenerarBoletaPDF(int id)
        {
            // Llama al método de negocio para generar el PDF
            byte[] pdfContent = await _ventaBussnies.GenerarBoletaPDF(id);

            // Devuelve el archivo PDF como una respuesta HTTP
            return File(pdfContent, "application/pdf", $"Comprobante-{id}.pdf");
        }

        [HttpGet]
        [Route("generate-detallesa-cocina/{id}")]
        public async Task<IActionResult> GenerarPDFDetallesCocina(int id)
        {
            // Llama al método de negocio para generar el PDF
            byte[] pdfContent = await _ventaBussnies.GenerarPDFDetallesCocina(id);

            // Devuelve el archivo PDF como una respuesta HTTP
            return File(pdfContent, "application/pdf", $"Para_Cocina-{id}.pdf");
        }





        #endregion GENERATE DOCUMENTS
    }
}
