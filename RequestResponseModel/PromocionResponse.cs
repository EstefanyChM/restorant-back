using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class PromocionResponse
	{
		public int Id { get; set; }
		public string UrlImagen { get; set; } = null!;
		public string Descripcion { get; set; } = null!;

		public string Nombre { get; set; } = null!;

		public DateOnly FechaInicio { get; set; }

		public DateOnly FechaFin { get; set; }

		public int Stock { get; set; }
		public decimal Total { get; set; }
		public decimal DctoPorcentaje { get; set; }



		public virtual ICollection<DetallesPromocion> DetallesPromocions { get; set; } = new List<DetallesPromocion>();
	}
}