using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("DetallePedido", Schema = "pedido")]
[Index("IdPedido", Name = "IX_DetallePedido_Id_Pedido")]
[Index("IdProducto", Name = "IX_DetallePedido_Id_Producto")]
public partial class DetallePedido
{
	[Key]
	public int Id { get; set; }

	public int Cantidad { get; set; }

	public int IdProducto { get; set; }

	public int IdPedido { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal PrecioUnitario { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal SubTotal { get; set; }
	[Column(TypeName = "decimal(4, 2)")]
	public decimal PorcentajeDescuentoPorUnidad { get; set; }

	public int? IdUsuarioSistema { get; set; }
	public bool? EstadoPreparacion { get; set; }


	[ForeignKey("IdPedido")]
	[InverseProperty("DetallePedidos")]
	public virtual Pedido IdPedidoNavigation { get; set; } = null!;

	[ForeignKey("IdProducto")]
	[InverseProperty("DetallePedidos")]
	public virtual Producto IdProductoNavigation { get; set; } = null!;

	[ForeignKey("IdUsuarioSistema")]
	[InverseProperty("DetallePedidos")]
	public virtual UsuariosSistema? IdUsuarioSistemaNavigation { get; set; }
}
