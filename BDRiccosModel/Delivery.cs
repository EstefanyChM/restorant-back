using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Delivery", Schema = "servicio")]
public partial class Delivery
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Reference { get; set; } = null!;

    public int IdPedido { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("Deliveries")]
    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
