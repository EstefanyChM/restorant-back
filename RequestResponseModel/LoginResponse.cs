using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class LoginResponse
    {
        public bool Success { get; set; } = false;
        public string Mensaje { get; set; } = "Usuario y/o password incorrecto";
        public string Token { get; set; } = "";
        public string TokenExpira { get; set; } = "";
        public UsuarioLoginResponse? Usuario { get; set; } = null;
        public RolResponse? Rol { get; set; } = null;
        public PersonaNaturalResponse? Persona { get; set; } = null;
        //retornar información del rol al que corresponde
    }
}

