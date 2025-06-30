using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Service", Schema = "servicio")]
public partial class Service
{
	[Key]
	public int Id { get; set; }

	[StringLength(20)]
	[Unicode(false)]
	public string Name { get; set; } = null!;

	[StringLength(10)]
	[Unicode(false)]
	public string Description { get; set; } = null!;
	//public bool Estado { get; set; }

}
