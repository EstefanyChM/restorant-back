using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RequestResponseModel
{
    public class ReservaRequest:IValidatableObject
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

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (FechaEntrada > FechaSalida)
			{
				yield return new ValidationResult("La Fecha de entrada no puede ser posterior a la Fecha de salida", new[] { "FechaEntrada", "FechaSalida" });
			}
		}


		/*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			// Verificar si el ID del cliente existe
			if (!ClienteExists(IdCliente))
			{
				yield return new ValidationResult("El ID del cliente proporcionado no existe", new[] { nameof(IdCliente) });
			}

			// Verificar si el ID de la habitación existe
			if (!HabitacionExists(IdHabitacion))
			{
				yield return new ValidationResult("El ID de la habitación proporcionado no existe", new[] { nameof(IdHabitacion) });
			}
		}

		private bool ClienteExists(int clienteId)
		{
			var dbContext = new TuDbContext();
			return dbContext.Clientes.Any(c => c.IdCliente == clienteId);
			return true; // Aquí, retornaré true siempre para demostración.
		}

		private bool HabitacionExists(int habitacionId)
		{
			// Aquí debes implementar la lógica para verificar si la habitación con el habitacionId proporcionado existe en tu base de datos.
			// Similar al método ClienteExists, utiliza tu contexto de base de datos para hacer la consulta necesaria.
			return true; // Aquí, retornaré true siempre para demostración.
		}*/



	}

	
}