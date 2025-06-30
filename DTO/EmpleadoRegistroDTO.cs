using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class EmpleadoRegistroDTO
	{
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int IdCargo { get; set; }

        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } // Mismo nombre que en IdentityUser

        [EmailAddress]
        public string Email { get; set; } // Mismo nombre que en IdentityUser

        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } // Nombre modificado para almacenar hash de contraseña

        [DataType(DataType.Password)]
        public string ConfirmarPassword { get; set; }

	}
}
