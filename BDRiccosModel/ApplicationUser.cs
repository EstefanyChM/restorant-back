using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BDRiccosModel
{
    public class ApplicationUser : IdentityUser
    //public class ApplicationUser : IdentityUser<string>
    {
        public bool EsPersonal { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
