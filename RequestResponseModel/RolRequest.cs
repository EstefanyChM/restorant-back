using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class RolRequest
    {
        public short Id { get; set; }
        [StringLength(5)]
        public string? Codigo { get; set; }
        [StringLength(255)]
        public string? Descripcion { get; set; }
        public string? Funcion { get; set; }
        [Required]
        public bool? IdEstado { get; set; }
        //[InverseProperty("IdRolNavigation")]
        //public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
        //[InverseProperty("IdRolNavigation")]
        //public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
