using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

using Validaciones;

namespace RequestResponseModel
{

    public class CategoriaRequest
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Disponibilidad { get; set; }
        public IFormFile? Foto { get; set; }
        public string? UrlImagen { get; set; }

	}
}


