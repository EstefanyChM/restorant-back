using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Pickup", Schema = "servicio")]
public partial class Pickup
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PickupTime { get; set; } = new DateTime(   ); 

    public int IdPedido { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("Pickups")]
    public virtual Pedido? IdPedidoNavigation { get; set; } = null!;
}
