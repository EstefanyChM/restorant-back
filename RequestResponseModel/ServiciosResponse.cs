using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class ServiciosResponse
	{


		public int Id { get; set; }
		public string Name { get; set; } = null!;


		public string Description { get; set; } = null!;


	}
}
