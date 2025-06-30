using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DTO
{
    public class UsuarioSistemaRegistrarDTO
    {

        public int IdPersonalEmpresa { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Range(1, 3, ErrorMessage = "El valor debe estar entre 1 y 4.")]
        public int Cargo { get; set; }

    }
}
