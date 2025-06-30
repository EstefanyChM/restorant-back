using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Empresa", Schema = "empresa")]
public partial class Empresa
{
	[Key]
	public int Id { get; set; }

	[StringLength(50)]
	[Unicode(false)]
	public string RazonSocial { get; set; } = null!;

	[Column("RUC")]
	[StringLength(11)]
	[Unicode(false)]
	public string Ruc { get; set; } = null!;

	[StringLength(100)]
	[Unicode(false)]
	public string? Direccion { get; set; }

	[StringLength(20)]
	[Unicode(false)]
	public string? Telefono { get; set; }

	[StringLength(20)]
	[Unicode(false)]
	public string? Correo { get; set; }

	[StringLength(300)]
	[Unicode(false)]
	public string? Urllogo { get; set; }

	[Column(TypeName = "datetime")]
	public DateTime? FechaRegistro { get; set; }

	[InverseProperty("IdEmpresaNavigation")]
	public virtual ICollection<HorariosRegulares> Horarios { get; set; } = new List<HorariosRegulares>();
}
