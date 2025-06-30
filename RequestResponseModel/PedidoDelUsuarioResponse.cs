using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class PedidoDelUsuarioResponse
	{
		public int IdProducto { get; set; }
		public string NombreProducto { get; set; }
		public int Cantidad { get; set; }
		public DateTime Fecha{ get; set; }
	}
}