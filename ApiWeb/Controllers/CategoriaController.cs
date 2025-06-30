using AutoMapper;
using IBussnies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel;
using System.Data.SqlClient;
using System.Data;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using BDRiccosModel;
using Bussnies;
using Repository;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]

    //[AllowAnonymous]
    public class CategoriaController : ControllerBase
    {

        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
        private readonly IMapper _mapper;
        private readonly ICategoriaBussnies _categoriaBussnies;

        public CategoriaController(IMapper mapper, ICategoriaBussnies categoriaBussnies)
        {
            _mapper = mapper;
            _categoriaBussnies = categoriaBussnies;

        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        string userRole = "";

        #region CRUD METHODS
        /// <summary>
        /// RETORNA: TABLA CATEGORIA
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>

        [HttpGet("gettto")]
        public async Task<ActionResult> Gettttto() // llamar a la vista
        {
            List<CategoriaResponse> categorias = await _categoriaBussnies.GetAll();
            return Ok(categorias);
        }


        /// <summary>
        /// RETORNA: TABLA CATEGORIA - Estado true
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>
        [AllowAnonymous]
        //[HttpGet]
        //[Route("Active")]
        [HttpGet("Activee")]

        public async Task<ActionResult> GetAllActive() // llamar a la vista
        {
            var a = await _categoriaBussnies.GetAllActive();

            return Ok(a);

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] bool? activo = null, bool? disponible = null)
        {
            List<CategoriaResponse> categorias = await _categoriaBussnies.GetAll();

            if (activo.HasValue)
            {
                categorias = categorias.Where(p => p.Estado == activo.Value).ToList();

                if (disponible.HasValue)
                {
                    //categorias = categorias.Where ( p => p.Disponibilidad == disponible.Value).ToList();
                    categorias = await _categoriaBussnies.GetAllActive();
                }
            }

            return Ok(categorias);
        }


        [AllowAnonymous]
        [HttpGet("NombreCategoria/{nombre}")]
        public async Task<ActionResult<int>> GetByName(string nombre)
        {
            Categoria categoria = await new CRUDRepository<Categoria>().Existe("Nombre", nombre); // PORQUE ME DA FLOJERA CREAR CLASES REPOSITORIO 

            return Ok(categoria.Id);
        }


        /// <summary>
        /// RETORNA: ROW CATEGORIA - POR Id - solo Administrador puede ver Estado = false
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>
        [HttpGet("{id}", Name = "ObtenerCategoria")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            return Ok(await _categoriaBussnies.GetById(id));
        }

        /// <summary>
        /// INSERTA: ROW CATEGORIA
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CategoriaCrearDTO categoriaCrearDTO)
        {
            var result = await _categoriaBussnies.Create(_mapper.Map<CategoriaRequest>(categoriaCrearDTO));
            return CreatedAtRoute("ObtenerCategoria", new { id = result.Id }, result);
            //return Ok(result);
        }




        /// <summary>
        /// ACTUALIZA: ROW CATEGORIA
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] CategoriaRequest request)
        {
            return Ok(await _categoriaBussnies.Update(request));
        }

        /// <summary>
        /// RETORNA:TABLA CARGO POR PAGINACIÓN Y FILTROS - SOLO ADMINISTRADOR MUESTRA TRUE Y FALSE
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilter([FromBody] GenericFilterRequest request)
        {
            userRole = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            //string userRolee = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;


            GenericFilterResponse<CategoriaResponse> res = await _categoriaBussnies.GetByFilteDependingRole(request, userRole);

            return Ok(res);
        }
        /// <summary>
        /// ELIMINA: lógigo - Column Estado
        /// </summary>
        /// <returns>List-CategoriaResponse</returns>
        //[Authorize(Roles = "Administrador")] 
        [HttpDelete("{id:int}/status")]
        public async Task<ActionResult> LogicDelete([FromRoute] int id)
        {
            var result = await _categoriaBussnies.LogicDelete(id);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            return Ok(_categoriaBussnies.Delete(id));
        }

        [HttpPatch]
        /*
        [
          {
            "path": "/estado",
            "op": "replace",
            "value": "false"
          }
        ]
       */
        public async Task<ActionResult> Patch([FromRoute] int id, JsonPatchDocument<CategoriaRequest> patchDocument)
        {
            return Ok(await _categoriaBussnies.Patch(id, patchDocument));
        }


        [HttpPost("multiple")]
        public async Task<ActionResult> CreateMultiple([FromBody] List<CategoriaRequest> request)
        {
            List<CategoriaResponse> res = await _categoriaBussnies.CreateMultiple(request);

            return Ok(res);
        }
        #endregion CRUD METHODS

    }
}
