using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel;

public class UsuariosSistemaRequest
{
    public int Id { get; set; }
    public int IdPersonalEmpresa { get; set; } = 0;
    public string IdPersonaNatural { get; set; } = "";
    public string Email { get; set; } = "";
    public bool Estado { get; set; } = false;
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Celular { get; set; }
    public string? Direccion { get; set; }
    public string NumeroDocumento { get; set; } = null!;
    public short IdPersonaTipoDocumento { get; set; }

    public List<string> Roles { get; set; }
    public string EstadoDescripcion
    {
        get
        {
            return Estado ? "Activo" : "Inactivo";
        }
    }
}





