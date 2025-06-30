using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class DetallePedidoRequest
	{
		public int Id { get; set; }
		public int Cantidad { get; set; }
		public int IdProducto { get; set; }
		public int IdPedido { get; set; }
		public decimal SubTotal => Cantidad * PrecioUnitario * (1 - PorcentajeDescuentoPorUnidad);
		public decimal PrecioUnitario { get; set; }
		public int? IdUsuarioSistema { get; set; }
		public bool? EstadoPreparacion { get; set; }
		public decimal PorcentajeDescuentoPorUnidad { get; set; }


		/*public decimal CalculoDePrecioFinal()
		{
			decimal totalPromo = 0;
			decimal totalNoPromo = 0;

			if (PorcentajeDescuentoPorUnidad !=0) { totalNoPromo+=Cantidad * PrecioUnitario;}
			else { totalPromo+=Cantidad * PrecioUnitario * (1-PorcentajeDescuentoPorUnidad); }

			return totalNoPromo + Math.Ceiling(totalPromo);

		}*/
	}

}