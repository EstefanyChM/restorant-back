using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

using Validaciones;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{

    public class EmpresaRequest
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string RUC { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Urllogo { get; set; }
        public TimeSpan? HoraApertura { get; set; }
        public TimeSpan? HoraCierre { get; set; }
        public IFormFile? Foto { get; set; }

	}
}


