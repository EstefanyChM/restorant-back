using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("MetodoPago", Schema = "pago")]
public partial class MetodoPago
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    [InverseProperty("IdMetodoPagoNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
