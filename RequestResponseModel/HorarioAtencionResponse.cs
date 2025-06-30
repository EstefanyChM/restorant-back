using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel;


public class HorarioAtencionResponse
{

	//HorariosRegulares horariosRegulares;
	//HorariosEspeciales horariosEspeciales;

	public int Id { get; set; }
	public string DiaSemana { get; set; }
	public string HoraApertura { get; set; }
	public string HoraCierre { get; set; }


}
