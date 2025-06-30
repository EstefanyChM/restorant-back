using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Keyless]
public partial class VPersona
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_persona_tipo_documento")]
    public short? IdPersonaTipoDocumento { get; set; }

    [Column("id_persona_tipo")]
    public short IdPersonaTipo { get; set; }

    [Column("nro_documento")]
    [StringLength(20)]
    [Unicode(false)]
    public string? NroDocumento { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("apellido_paterno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ApellidoPaterno { get; set; }

    [Column("apellido_materno")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ApellidoMaterno { get; set; }

    [Column("nombre_completo")]
    [StringLength(180)]
    [Unicode(false)]
    public string? NombreCompleto { get; set; }

    [Column("fecha_nacimiento")]
    [Precision(6)]
    public DateTime FechaNacimiento { get; set; }

    [Column("email")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("celular")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Celular { get; set; }

    [Column("direccion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [Column("id_estado")]
    public bool IdEstado { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    [Column("usuario_crea")]
    public int UsuarioCrea { get; set; }

    [Column("usuario_actualiza")]
    public int UsuarioActualiza { get; set; }

    [Column("fecha_crea")]
    [Precision(6)]
    public DateTime FechaCrea { get; set; }

    [Column("fecha_actualiza")]
    [Precision(6)]
    public DateTime FechaActualiza { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PersonaTipoDocumento { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string? PersonaTipo { get; set; }
}
