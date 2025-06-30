using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("HorariosRegulares", Schema = "empresa")]
[Index("IdEmpresa", Name = "Empresa_HorariosRegulares")]

public partial class HorariosRegulares
{
	[Key]
	public int Id { get; set; }

	public string DiaSemana { get; set; }
	[DataType(DataType.Time)]
	public TimeSpan HoraApertura { get; set; }

	[DataType(DataType.Time)]
	public TimeSpan HoraCierre { get; set; }

	public int IdEmpresa { get; set; }

	[ForeignKey("IdEmpresa")]
	[InverseProperty("Horarios")]
	public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

}
