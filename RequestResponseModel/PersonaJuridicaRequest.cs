using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class PersonaJuridicaRequest
    {
        public int IdPersonaJuridica { get; set; }
        public string Ruc { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string? NombreComercial { get; set; } = null!;
        public string? Tipo { get; set; } = null!;
        public string? Estado { get; set; } = null!;
        public string? Condicion { get; set; } = null!;
        public string? Direccion { get; set; } = null!;
        public string? Departamento { get; set; } = null!;
        public string? Provincia { get; set; } = null!;
        public string? Distrito { get; set; } = null!;
        public string? Ubigeo { get; set; } = null!;
        public string? Capital { get; set; } = null!;
        public short? IdPersonaTipoDocumento { get; set; }
    }
}



