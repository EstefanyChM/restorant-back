using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
	public class AutenticacionResponse
	{
		public string Token { get; set; } //
        public DateTime Expiracion { get; set; } //
        public bool EsPersonal { get; set; }
        public int IdUsuario { get; set; }
    }
}
