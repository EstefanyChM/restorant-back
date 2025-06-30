using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
	public class PedidoResponse
	{
		public int Id { get; set; }

		public bool Estado { get; set; }
		public byte IdServicio { get; set; }
		public decimal Total { get; set; }
		public bool Finalizado { get; set; }
		public bool VentaFinalizada { get; set; }
		public TimeSpan HoraEntrada { get; set; }


		public List<DetallePedidoResponse> DetallePedidoResp { get; set; }
		//public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
		// public virtual ICollection<Pickup> Pickups { get; set; } = new List<Pickup>();




	}
}