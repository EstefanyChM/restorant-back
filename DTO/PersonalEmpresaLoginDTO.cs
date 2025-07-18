﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class PersonalEmpresaLoginDTO
	{
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password{ get; set; }

        public int Cargo { get; set; }
    }
}
