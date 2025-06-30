using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("EstadoMensaje", Schema = "mensaje")]
public partial class EstadoMensaje
{
	[Key]
	public int Id { get; set; }

	[StringLength(15)]
	[Unicode(false)]
	public string Nombre { get; set; }

	[InverseProperty("IdEstadoMensajeNavigation")]
	public virtual ICollection<MensajeContacto> MensajeContactos { get; set; } = new List<MensajeContacto>();
}
