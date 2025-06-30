using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
	public class DetallesPromocionResponse
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