using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class DetallesPromocionRequest
	{
		public int Id { get; set; }

		public int IdPromocion { get; set; }
		public decimal? PorcentajeDescuentoPorUnidad { get; set; }


		public int IdProducto { get; set; }

		public int Cantidad { get; set; }
		public decimal PrecioUnitario { get; set; }

		public decimal SubTotal { get; set; }

	}
}