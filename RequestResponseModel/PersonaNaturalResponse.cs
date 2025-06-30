using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class PersonaNaturalResponse
    {
         public int IdPersonaNatural { get; set; }
         public string? Nombre { get; set; }
         public string? Apellidos { get; set; }
         public DateTime? FechaNacimiento { get; set; }
         public string? Celular { get; set; }
         public string? Direccion { get; set; }
         public string NumeroDocumento { get; set; } = null!;
         public short? IdPersonaTipoDocumento { get; set; }
    }
}
