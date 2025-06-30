using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Categoria", Schema = "producto")]
public partial class Categoria
{
	[Key]
	public int Id { get; set; }

	[StringLength(50)]
	[Unicode(false)]
	public string Nombre { get; set; } = null!;

	[StringLength(100)]
	[Unicode(false)]
	public string Descripcion { get; set; } = null!;

	public bool Disponibilidad { get; set; }

	public bool Estado { get; set; }

	[StringLength(300)]
	[Unicode(false)]
	public string UrlImagen { get; set; } = null!;

	[InverseProperty("IdCategoriaNavigation")]
	public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
