using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Keyless]
public partial class ClientePersonaJuridica
{
    public bool Estado { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Ruc { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string RazonSocial { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string NombreComercial { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Tipo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EstadoPersonaJuridica { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Condicion { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Direccion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Departamento { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Provincia { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Distrito { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Ubigeo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Capital { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    public int IdPersonaJuridica { get; set; }

    public int Id { get; set; }
}
