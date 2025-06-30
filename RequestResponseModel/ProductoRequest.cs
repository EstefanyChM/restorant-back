using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Validaciones;

namespace RequestResponseModel
{
	public class ProductoRequest
	{
		public int Id { get; set; }

		[StringLength(50)]
		public string Nombre { get; set; } = null!;

		[StringLength(100)]
		public string Descripcion { get; set; }

		public decimal Precio { get; set; }
		public int Stock { get; set; }
		public int IdCategoria { get; set; }

		[Column(TypeName = "decimal(4, 2)")]
		public decimal MargenGanancia { get; set; }

		[StringLength(300)]
		public string? UrlImagen { get; set; }

		public bool Estado { get; set; }

		public bool Disponibilidad { get; set; }

		//[PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
		//[TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
		public IFormFile? Foto { get; set; }
	}
}
