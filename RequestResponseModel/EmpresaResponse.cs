using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{

	public class EmpresaResponse
	{
		public int Id { get; set; }
		public string RazonSocial { get; set; } = "";
		public string RUC { get; set; } = "";
		public string? Direccion { get; set; } = "";
		public string? Telefono { get; set; } = "";
		public string? Correo { get; set; } = "";
		public string? Urllogo { get; set; } = "";

		// Cambiar a TimeOnly? para que coincida con la clase Empresa
		public DateTime? FechaRegistro { get; set; } = DateTime.Now;

		public List<HorariosRegulares> Horarios { get; set; } = new List<HorariosRegulares>();

	}

}
