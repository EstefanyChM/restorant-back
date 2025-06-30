using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Promocion", Schema = "promocion")]
public partial class Promocion
{
	[Key]
	public int Id { get; set; }

	[StringLength(300)]
	[Unicode(false)]
	public string UrlImagen { get; set; } = null!;

	public DateOnly FechaInicio { get; set; }

	public DateOnly FechaFin { get; set; }

	[StringLength(300)]
	[Unicode(false)]
	public string Descripcion { get; set; } = null!;

	[StringLength(50)]
	[Unicode(false)]
	public string Nombre { get; set; } = null!;

	[Column(TypeName = "decimal(10, 2)")]
	public decimal Total { get; set; }

	[Column(TypeName = "decimal(5, 2)")]
	public decimal DctoPorcentaje { get; set; }

	public int Stock { get; set; }



	[InverseProperty("IdPromocionNavigation")]
	public virtual ICollection<DetallesPromocion> DetallesPromocions { get; set; } = new List<DetallesPromocion>();
}
