using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Producto", Schema = "producto")]
[Index("IdCategoria", Name = "IX_Carta_Id_Categoria")]
public partial class Producto
{
	[Key]
	public int Id { get; set; }

	[StringLength(50)]
	[Unicode(false)]
	public string Nombre { get; set; } = null!;

	[StringLength(100)]
	[Unicode(false)]
	public string Descripcion { get; set; } = null!;

	[Column(TypeName = "decimal(10, 2)")]
	public decimal Precio { get; set; }

	public int IdCategoria { get; set; }
	[StringLength(300)]
	[Unicode(false)]
	public string UrlImagen { get; set; } = null!;

	public bool Estado { get; set; }

	public bool Disponibilidad { get; set; }

	public int Stock { get; set; }


	[Column(TypeName = "decimal(4, 2)")]
	public decimal MargenGanancia { get; set; }



	[InverseProperty("IdProductoNavigation")]
	public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

	[InverseProperty("IdProductoNavigation")]
	public virtual ICollection<DetallesPromocion> DetallesPromocions { get; set; } = new List<DetallesPromocion>();

	[ForeignKey("IdCategoria")]
	[InverseProperty("Productos")]
	public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
