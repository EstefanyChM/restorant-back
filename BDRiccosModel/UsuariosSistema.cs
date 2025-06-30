using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("UsuariosSistema", Schema = "persona")]



public partial class UsuariosSistema
{
    [Key]
    public int Id { get; set; }

    public int IdPersonalEmpresa { get; set; }

    [StringLength(450)]
    public string IdApplicationUser { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;

    public bool Estado { get; set; }

    [ForeignKey("IdApplicationUser")]
    //[InverseProperty("UsuariosSistemas")]
    public virtual ApplicationUser IdApplicationUserNavigation { get; set; } = null!;
    //public virtual AspNetUser IdApplicationUserNavigation { get; set; } = null!;

    

    [ForeignKey("IdPersonalEmpresa")]
    [InverseProperty("UsuariosSistemas")]
    public virtual PersonalEmpresa IdPersonalEmpresaNavigation { get; set; } = null!;



      [InverseProperty("IdUsuarioSistemaNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}





