using System;
using System.Runtime.Serialization;

namespace ExcepcionesPersonalizadas
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        // Propiedad para almacenar el recurso que no se encontró
        public string? Recurso { get; }

        // Constructor sin parámetros
        public ResourceNotFoundException()
        {
        }

        // Constructor que acepta solo un mensaje
        public ResourceNotFoundException(string? message) : base(message)
        {
        }

        // Constructor que acepta el recurso y el mensaje
        public ResourceNotFoundException(string recurso, string? message) : base(message)
        {
            Recurso = recurso;
        }

        // Constructor que acepta recurso, mensaje y una excepción interna
        public ResourceNotFoundException(string recurso, string? message, Exception? innerException)
            : base(message, innerException)
        {
            Recurso = recurso;
        }

        // Constructor protegido para serialización
        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Recuperar el valor de la propiedad `Recurso` de la información de serialización
            Recurso = info.GetString("Recurso");
        }

        // Sobrescribir el método GetObjectData para serializar la propiedad `Recurso`
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Recurso", Recurso); // Agregar `Recurso` a los datos de serialización
        }
    }
}
