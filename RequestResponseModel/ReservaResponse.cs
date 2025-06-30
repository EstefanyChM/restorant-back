using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class ReservaResponse
	{
		public int Id { get; set; }

		public DateTime FechaEntrada { get; set; }

		public DateTime FechaSalida { get; set; }

		public double TotalPagar { get; set; }

		public bool Estado { get; set; }

		public int IdCliente { get; set; }

		public int IdHabitacion { get; set; }
		/*
		[ForeignKey("IdCliente")]
		[InverseProperty("Reservas")]
		public virtual Cliente IdClienteNavigation { get; set; } = null!;

		[ForeignKey("IdHabitación")]
		[InverseProperty("Reservas")]
		public virtual Habitacion IdHabitaciónNavigation { get; set; } = null!;

		[InverseProperty("IdReservaNavigation")]
		public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
		*/
	}
}