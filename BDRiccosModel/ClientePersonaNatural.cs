using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Keyless]
public partial class ClientePersonaNatural
{
    public int Id { get; set; }

    public int IdPersonaNatural { get; set; }

    public bool Estado { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Apellidos { get; set; }

    [Precision(6)]
    public DateTime? FechaNacimiento { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Celular { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string NumeroDocumento { get; set; } = null!;

    public short? IdPersonaTipoDocumento { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;
}
