using BDRiccosModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
	public class RemiteRequest
	{
		public string Email { get; set; }
		public string Nombre { get; set; }
	}
}


