
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Validaciones;

namespace DTO
{
	public class CategoriaCrearDTO
	{
		//[Required(ErrorMessage = "El campo {0} es requerido")]
		//[StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
		public string? Nombre { get; set; }
		//[Required(ErrorMessage = "El campo {0} es requerido")]
		//[StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
		public string? Descripcion { get; set; }
		//[Required(ErrorMessage = "El campo {0} es requerido")]
		//[PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
		//[TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
		public IFormFile? Foto { get; set; }
	}
}
