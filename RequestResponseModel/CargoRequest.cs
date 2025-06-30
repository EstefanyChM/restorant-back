using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RequestResponseModel
{
    public class CargoRequest
    {
        public int IdCargo { get; set; }
        [StringLength(30)]
        public string? Codigo { get; set; }
        [StringLength(50)]
        public string? Nombre { get; set; }
        [Required]
        public bool? IdEstado { get; set; }
        //public virtual ICollection<Colaborador> Colaboradors { get; set; } = new List<Colaborador>();
    }
}