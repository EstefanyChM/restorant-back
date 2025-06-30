using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("OnlineUser", Schema = "persona")]
public partial class OnlineUser
{
    [Key]
    public int Id { get; set; }

    public int IdPersona { get; set; }

    [StringLength(450)]
    public string IdApplicationUser { get; set; } = null!;

    public bool Estado { get; set; }

    [StringLength(256)]
    public string Email { get; set; } = null!;

    public int IdCliente { get; set; }

    [ForeignKey("IdApplicationUser")]
    //[InverseProperty("OnlineUsers")]
    public virtual ApplicationUser IdApplicationUserNavigation { get; set; } = null!;

    [ForeignKey("IdCliente")]
    [InverseProperty("OnlineUsers")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
