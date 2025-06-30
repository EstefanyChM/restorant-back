using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class PedidoRequest
	{
		public int Id { get; set; }

		public bool Finalizado { get; set; }

		public bool VentaFinalizada { get; set; }

		public byte IdServicio { get; set; }


		public bool Estado { get; set; }


		//public decimal Total => CalculoDeTotalConYSinPromo();
		public decimal Total => DetallePedidosRequest?.Sum(dp => dp.SubTotal) ?? 0;
		public TimeSpan? HoraEntrada { get; set; }

		public List<DetallePedidoRequest> DetallePedidosRequest { get; set; }


		/*public decimal CalculoDeTotalConYSinPromo()
		{
			decimal ConDescuento, SinDescuento;



			return 0;

		}*/
	}
}