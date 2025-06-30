using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class OnlineUserRegistrarDTO
	{
		public string Nombre { get; set; }
        public string Apellidos { get; set; }
        [Range(1, 4, ErrorMessage = "El valor debe estar entre 1 y 4.")]
        public short IdPersonaTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password{ get; set; }
        //public string IdIdentityUser { get; set; }

	}
}
