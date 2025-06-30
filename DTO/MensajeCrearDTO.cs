
using Microsoft.AspNetCore.Http;
using Validaciones;

namespace DTO
{
	public class MensajeCrearDTO
	{
        public string RemiteNombre { get; set; }
        public string RemiteEmail { get; set; }
        public int IdAsunto { get; set; }
        public string? Mensaje { get; set; }
	}
}
