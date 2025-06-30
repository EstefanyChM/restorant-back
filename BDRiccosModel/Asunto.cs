using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Asunto", Schema = "mensaje")]
public partial class Asunto
{
	[Key]
	public int Id { get; set; }

	[StringLength(20)]
	[Unicode(false)]
	public string Nombre { get; set; }

	[InverseProperty("IdAsuntoNavigation")]
	public virtual ICollection<MensajeContacto> MensajeContactos { get; set; } = new List<MensajeContacto>();
}
