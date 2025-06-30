using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Persona", Schema = "persona")]
[Index("IdPersonaTipoDocumento", Name = "IX_persona_id_persona_tipo_documento")]
public partial class Persona
{
    [Key]
    public int Id { get; set; }

    public short? IdPersonaTipoDocumento { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Apellidos { get; set; }

    [StringLength(180)]
    [Unicode(false)]
    public string? NombreCompleto { get; set; }

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

    [InverseProperty("IdPersonaNavigation")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [ForeignKey("IdPersonaTipoDocumento")]
    [InverseProperty("Personas")]
    public virtual PersonaTipoDocumento? IdPersonaTipoDocumentoNavigation { get; set; }

    [InverseProperty("IdPersonaNavigation")]
    public virtual ICollection<OnlineUser> OnlineUsers { get; set; } = new List<OnlineUser>();

    [InverseProperty("IdPersonaNavigation")]
    public virtual ICollection<PersonalEmpresa> PersonalEmpresas { get; set; } = new List<PersonalEmpresa>();
}
