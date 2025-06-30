using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
    public partial class EmailSuscriptorResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        
        public bool Estado { get; set; } = false;
        public DateTime FechaSuscripcion { get; set; } = DateTime.Now;
        public DateTime? FechaDesuscripcion { get; set; }  = DateTime.Now;
       
    }
}
    