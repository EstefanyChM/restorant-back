using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("HorariosEspeciales", Schema = "empresa")]
public partial class HorariosEspeciales
{
	[Key]
	public int Id { get; set; }

	[DataType(DataType.Date)]
	public DateTime Fecha { get; set; }

	[DataType(DataType.Time)]
	public TimeSpan HoraApertura { get; set; }

	[DataType(DataType.Time)]
	public TimeSpan HoraCierre { get; set; }
}