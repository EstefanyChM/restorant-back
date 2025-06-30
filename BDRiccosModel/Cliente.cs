using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Cliente", Schema = "persona")]
public partial class Cliente
{
    [Key]
    public int Id { get; set; }

    public bool Estado { get; set; }

    public short IdTipoPersona { get; set; }

    public int? IdTablaPersonaNatural { get; set; }

    public int? IdTablaPersonaJuridica { get; set; }

    [ForeignKey("IdTablaPersonaJuridica")]
    [InverseProperty("Clientes")]
    public virtual PersonaJuridica? IdTablaPersonaJuridicaNavigation { get; set; }

    [ForeignKey("IdTablaPersonaNatural")]
    [InverseProperty("Clientes")]
    public virtual PersonaNatural? IdTablaPersonaNaturalNavigation { get; set; }

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<OnlineUser> OnlineUsers { get; set; } = new List<OnlineUser>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
