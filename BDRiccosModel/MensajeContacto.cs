using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("MensajeContacto", Schema = "mensaje")]
[Index("IdAsunto", Name = "IX_MensajeContacto_id_Asunto")]
[Index("IdEstadoMensaje", Name = "IX_MensajeContacto_id_estado")]
public partial class MensajeContacto
{
	[Key]
	public int Id { get; set; }

	public int IdAsunto { get; set; }

	public int IdRemite { get; set; }

	[StringLength(100)]
	[Unicode(false)]
	public string Mensaje { get; set; } = null!;

	public DateOnly Fecha { get; set; }

	public int IdEstadoMensaje { get; set; }

	public bool Estado { get; set; }

	[ForeignKey("IdAsunto")]
	[InverseProperty("MensajeContactos")]
	public virtual Asunto IdAsuntoNavigation { get; set; } = null!;

	[ForeignKey("IdEstadoMensaje")]
	[InverseProperty("MensajeContactos")]
	public virtual EstadoMensaje IdEstadoMensajeNavigation { get; set; } = null!;

	[ForeignKey("IdRemite")]
	[InverseProperty("MensajeContactos")]
	public virtual Remite IdRemiteNavigation { get; set; } = null!;
}
