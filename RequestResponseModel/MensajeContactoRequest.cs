using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class MensajeContactoRequest
    {
        public int Id { get; set; }
        public int IdAsunto { get; set; }
        public int IdRemite { get; set; }
        [StringLength(100)]
        public string Mensaje { get; set; } = null!;
        public DateOnly Fecha { get; set; }
        public int IdEstadoMensaje { get; set; }
        public bool Estado { get; set; }


        /********************/
        public string RemiteEmail { get; set; } = null!;
        public string RemiteNombre { get; set; } = null!;

    }
}