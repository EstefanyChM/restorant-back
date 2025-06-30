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
	public class DetallePedidoResponse
	{
		public int Id { get; set; }
		public int Cantidad { get; set; }
		public int IdProducto { get; set; }
		public string NombreProducto { get; set; }
		public int IdCategoria { get; set; }
		public string NombreCategoria { get; set; }
		public int IdPedido { get; set; }
		public decimal SubTotal { get; set; }
		public decimal PrecioUnitario { get; set; }
		public int IdUsuarioSistema { get; set; }

		public bool EstadoPreparacion { get; set; }


	}
}