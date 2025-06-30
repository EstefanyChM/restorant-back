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
	public class ProductoResponse
	{
		public int Id { get; set; }

		public string Nombre { get; set; } = "";
		public string Descripcion { get; set; } = "";
		public decimal Precio { get; set; } = 0;
		public int IdCategoria { get; set; } = 0;
		public string? CategoriaNombre { get; set; } = "";
		public int Stock { get; set; }
		[Column(TypeName = "decimal(4, 2)")]
		public decimal MargenGanancia { get; set; }

		public string UrlImagen { get; set; } = "";
		public bool Disponibilidad { get; set; } = false;

		public string DisponibilidadDescripcion
		{
			get
			{
				return Disponibilidad ? "Disponible" : "No Disponible";
			}
		}

		public bool Estado { get; set; } = false;
		public string EstadoDescripcion
		{
			get
			{
				return Estado ? "Activo" : "Inactivo";
			}
		}

		public virtual ICollection<DetallePedido> DetallePedidos { get; set; }

	}
}
