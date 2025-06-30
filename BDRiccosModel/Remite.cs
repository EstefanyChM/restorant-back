using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Remite", Schema = "mensaje")]
public partial class Remite
{
	[Key]
	public int Id { get; set; }

	[StringLength(50)]
	[Unicode(false)]
	public string Email { get; set; } = null!;

	[StringLength(15)]
	[Unicode(false)]
	public string Nombre { get; set; } = null!;

	[InverseProperty("IdRemiteNavigation")]
	public virtual ICollection<MensajeContacto> MensajeContactos { get; set; } = new List<MensajeContacto>();
}
