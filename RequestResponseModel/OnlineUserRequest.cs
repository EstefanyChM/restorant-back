using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CommonModel
{
	public class OnlineUserRequest
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; }

    [StringLength(100)]
    public string Apellidos { get; set; }

    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }


    public string IdentityUserId { get; set; }
    //public IdentityUser IdentityUser { get; set; }
}

	
}
