using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Pedido", Schema = "pedido")]
public partial class Pedido
{
	[Key]
	public int Id { get; set; }

	public bool Estado { get; set; }

	public byte IdServicio { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal Total { get; set; }

	public bool Finalizado { get; set; }

	public bool VentaFinalizada { get; set; }

	[InverseProperty("IdPedidoNavigation")]
	public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

	[InverseProperty("IdPedidoNavigation")]
	public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

	[InverseProperty("IdPedidoNavigation")]
	public virtual ICollection<EnTienda> EnTienda { get; set; } = new List<EnTienda>();

	[InverseProperty("IdPedidoNavigation")]
	public virtual ICollection<Pickup> Pickups { get; set; } = new List<Pickup>();

	[InverseProperty("IdPedidoNavigation")]
	public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

	public TimeSpan? HoraEntrada { get; set; }

}
