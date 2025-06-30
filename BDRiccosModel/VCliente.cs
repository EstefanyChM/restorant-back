using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Keyless]
public partial class VCliente
{
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    public bool? Estado { get; set; }

    [Column("id_persona")]
    public int IdPersona { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("apellido_paterno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Apellidos { get; set; }

    [Column("fecha_nacimiento")]
    [Precision(6)]
    public DateTime? FechaNacimiento { get; set; }

    [Column("email")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("celular")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Celular { get; set; }

    [Column("id_persona_tipo_documento")]
    public short? IdPersonaTipoDocumento { get; set; }

    [Column("codigo")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Codigo { get; set; }

    [Column("nro_documento")]
    [StringLength(20)]
    [Unicode(false)]
    public string? NroDocumento { get; set; }

    [Column("id_persona_tipo")]
    public short? IdPersonaTipo { get; set; }

    [Column("descripcion")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Descripcion { get; set; }
}
