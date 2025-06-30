using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Keyless]
public partial class VistaDetallePedido
{
    public int Id { get; set; }

    public int IdPedido { get; set; }

    public int IdProducto { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreProducto { get; set; } = null!;

    public int IdCategoria { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreCategoria { get; set; } = null!;

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public int IdVenta { get; set; }
}
