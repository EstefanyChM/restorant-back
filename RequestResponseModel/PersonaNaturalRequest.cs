using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class PersonaNaturalRequest
    {
        public int IdPersonaNatural { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public short IdPersonaTipoDocumento { get; set; }
    }
}



