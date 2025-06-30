using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
    public class MensajeContactoResponse
    {
        public int Id { get; set; }
        public string AsuntoNombre { get; set; } = "";
        public string RemiteEmail { get; set; } = "";
        public string Mensaje { get; set; } = "";
        public DateOnly Fecha { get; set; }
        public string MensajeEstado { get; set; }="";
        public bool Estado { get; set; } = false;
    }
}