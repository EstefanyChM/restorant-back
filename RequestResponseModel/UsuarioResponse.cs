using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? ChangePassword { get; set; }
        public string? Email { get; set; }
        public short IdRol { get; set; }
        public string? IdentificadorCelular { get; set; }
        public bool IdEstado { get; set; }
        public int UsuarioCrea { get; set; }
        public int UsuarioActualiza { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaActualiza { get; set; }

        //[InverseProperty("IdUsuarioActualizaNavigation")]
        //public virtual ICollection<Documento> DocumentoIdUsuarioActualizaNavigations { get; set; } = new List<Documento>();

        //[InverseProperty("IdUsuarioRegistroNavigation")]
        //public virtual ICollection<Documento> DocumentoIdUsuarioRegistroNavigations { get; set; } = new List<Documento>();

        //[InverseProperty("UsuarioActualizaNavigation")]
        //public virtual ICollection<Error> ErrorUsuarioActualizaNavigations { get; set; } = new List<Error>();

        //[InverseProperty("UsuarioCreaNavigation")]
        //public virtual ICollection<Error> ErrorUsuarioCreaNavigations { get; set; } = new List<Error>();

        //[ForeignKey("IdPersona")]
        //[InverseProperty("Usuarios")]
        //public virtual Persona IdPersonaNavigation { get; set; } = null!;

        //[ForeignKey("IdRol")]
        //[InverseProperty("Usuarios")]
        //public virtual Rol IdRolNavigation { get; set; } = null!;

        //[InverseProperty("IdUserNavigation")]
        //public virtual ICollection<UserSesion> UserSesions { get; set; } = new List<UserSesion>();
    }
}
