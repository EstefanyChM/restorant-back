using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("EstadoVenta", Schema = "venta")]
public partial class EstadoVenta
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdEstadoVentaNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
