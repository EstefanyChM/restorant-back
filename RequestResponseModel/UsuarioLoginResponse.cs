using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class UsuarioLoginResponse
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? ChangePassword { get; set; }
        public string? Email { get; set; }
        public string? IdentificadorCelular { get; set; }
        public bool IdEstado { get; set; }
    }
}
