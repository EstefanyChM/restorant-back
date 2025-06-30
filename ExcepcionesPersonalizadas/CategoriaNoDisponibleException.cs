using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPersonalizadas
{

    [Serializable] // Marca la clase como serializable
    public class CategoriaNoDisponibleException : Exception
    {
        // Constructor básico
        public CategoriaNoDisponibleException(string message)
            : base(message)
        {
        }
    
        // Constructor con excepción interna
        public CategoriaNoDisponibleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    
        // Constructor necesario para la deserialización
        protected CategoriaNoDisponibleException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    
        // Propiedad opcional para agregar más detalles
        public string CategoriaId { get; set; }
    
        // Sobrescribir el método ToString para mayor detalle
        public override string ToString()
        {
            return $"Error: {Message} | CategoriaId: {CategoriaId ?? "No especificado"}";
        }
    }

}
