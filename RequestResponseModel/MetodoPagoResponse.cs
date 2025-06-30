using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class MetodoPagoResponse
	{
        public int IdMetodoPago { get; set; }
		public string Nombre { get; set; } = "";
    }
}