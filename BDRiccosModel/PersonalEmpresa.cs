using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("PersonalEmpresa", Schema = "persona")]
[Index("IdPersona", Name = "IX_PersonalEmpresa_IdPersona")]
public partial class PersonalEmpresa
{
    [Key]
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public bool Estado { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [ForeignKey("IdPersona")]
    [InverseProperty("PersonalEmpresas")]
    public virtual PersonaNatural IdPersonaNavigation { get; set; } = null!;

    [InverseProperty("IdPersonalEmpresaNavigation")]
    public virtual ICollection<UsuariosSistema> UsuariosSistemas { get; set; } = new List<UsuariosSistema>();
}
