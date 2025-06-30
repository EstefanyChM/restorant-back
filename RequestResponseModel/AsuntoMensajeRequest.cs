using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class AsuntoMensajeRequest
	{
		public int Id { get; set; }

		public string Nombre { get; set; }

	}
}
