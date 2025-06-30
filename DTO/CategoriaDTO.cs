
using Microsoft.AspNetCore.Http;
using Validaciones;

namespace DTO
{
	public class CategoriaDTO
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Disponibilidad { get; set; }
        public bool Estado { get; set; }
        public string UrlImagen { get; set; }
	}
}
