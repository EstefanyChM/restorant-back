using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModel
{
    public class PrioridadResponse
    {
        public short Id { get; set; }
        [StringLength(5)]
        public string? Codigo { get; set; }
        [StringLength(50)]
        public string? Descripcion { get; set; }
        public short? DiasAtencion { get; set; }
        public bool? IdEstado { get; set; }

        // [InverseProperty("IdPrioridadNavigation")]
        // public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}
