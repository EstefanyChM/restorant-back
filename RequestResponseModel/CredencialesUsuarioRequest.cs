using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
	public class CredencialesUsuarioRequest
	{
		[Required]
        [EmailAddress]
        public string Email { get; set; }
       [Required]
        public string Password { get; set; }

        public string? Cargo { get; set; }

        public bool? rememberMe{ get; set; }

        public bool? keepMeLogged { get; set; }
    }
}
