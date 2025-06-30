using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

using Validaciones;
using BDRiccosModel;

namespace RequestResponseModel
{

	public class PromocionRequest
	{
		public int Id { get; set; }
		//public IFormFile? Foto { get; set; }
		public string UrlImagen { get; set; } = null!;
		public string Descripcion { get; set; } = null!;
		public string Nombre { get; set; } = null!;

		public int Stock { get; set; }

		public DateOnly FechaInicio { get; set; }
		public DateOnly FechaFin { get; set; }
		public decimal DctoPorcentaje { get; set; }

		public decimal Total { get; set; }

		public List<DetallesPromocionRequest> DetallesPromocions { get; set; } = new List<DetallesPromocionRequest>();

		// public virtual ICollection<DetallesPromocion> DetallesPromocions { get; set; } = [];

		// Campo temporal para recibir el JSON de los detalles
		//public string DetallesPromocionsJson { get; set; } = string.Empty;

		// Propiedad deserializada
		//[NotMapped]
		//public virtual ICollection<DetallesPromocion> DetallesPromocions { get; set; } = new List<DetallesPromocion>();
	}
}


