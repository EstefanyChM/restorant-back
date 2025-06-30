using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("EnTienda", Schema = "servicio")]
public partial class EnTienda
{
    [Key]
    public int Id { get; set; }

    public int IdMesa { get; set; }

    public int IdPedido { get; set; }

    [ForeignKey("IdMesa")]
    [InverseProperty("EnTienda")]
    public virtual Mesa IdMesaNavigation { get; set; } = null!;

    [ForeignKey("IdPedido")]
    [InverseProperty("EnTienda")]
    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
