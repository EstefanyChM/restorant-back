using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public partial class EmailSuscriptorRequest
    {
        public int Id { get; set; }
		[EmailAddress]
        public string Email { get; set; } 
        public bool Estado { get; set; } 
        public DateTime FechaSuscripcion { get; set; } 
        public DateTime? FechaDesuscripcion { get; set; } 
    }

}
