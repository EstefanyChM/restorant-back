using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BDRiccosModel;

[Table("DetallesPromocion", Schema = "promocion")]
public partial class DetallesPromocion
{
	[Key]
	public int Id { get; set; }

	public int IdPromocion { get; set; }

	public int IdProducto { get; set; }

	public int Cantidad { get; set; }
	[Column(TypeName = "decimal(10, 2)")]
	public decimal PrecioUnitario { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal SubTotal { get; set; }


	[Column(TypeName = "decimal(4, 2)")]
	public decimal? PorcentajeDescuentoPorUnidad { get; set; }

	[ForeignKey("IdProducto")]
	[InverseProperty("DetallesPromocions")]
	public virtual Producto IdProductoNavigation { get; set; } = null!;


	[ForeignKey("IdPromocion")]
	[InverseProperty("DetallesPromocions")]
	public virtual Promocion IdPromocionNavigation { get; set; } = null!;
}
