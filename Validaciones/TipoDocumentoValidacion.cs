using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validaciones
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class TipoDocumentoValidacion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance; // Se obtiene el objeto completo


            var tipoDocumentoProperty = instance.GetType().GetProperty("IdPersonaTipoDocumento");
            if (tipoDocumentoProperty == null)
            {
                return new ValidationResult("No se encontró el tipo de documento.");
            }

            short tipoDocumento = (short)tipoDocumentoProperty.GetValue(instance); // Se obtiene el valor de IdPersonaTipoDocumento
            string numeroDocumento = value.ToString();

            return tipoDocumento switch
            {
                1 when numeroDocumento.Length != 8 => new ValidationResult("El DNI debe tener 8 dígitos."),
                2 when numeroDocumento.Length != 11 => new ValidationResult("El RUC debe tener 11 dígitos."),
                3 when numeroDocumento.Length != 9 => new ValidationResult("El Carné de extranjería debe tener 9 dígitos."),
                _ => ValidationResult.Success // Pasaporte o valores no restringidos en longitud
            };

        }

    }


}
