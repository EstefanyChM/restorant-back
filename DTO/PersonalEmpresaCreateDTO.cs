using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validaciones;

namespace DTO
{
    public class PersonalEmpresaCreateDTO
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }

        [TipoDocumentoValidacion]
        public string NumeroDocumento { get; set; } = null!;
        public short IdPersonaTipoDocumento { get; set; }
        public string? Email { get; set; }

    }
}
