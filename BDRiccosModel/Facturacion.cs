using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Facturacion", Schema = "pago")]
public partial class Facturacion
{
    [Key]
    [Column("ID_Factura")]
    public int IdFactura { get; set; }

    [Column("ID_Pedido")]
    public int? IdPedido { get; set; }

    [Column("Fecha_Factura", TypeName = "date")]
    public DateTime? FechaFactura { get; set; }

    [Column("Total_Factura", TypeName = "decimal(10, 2)")]
    public decimal? TotalFactura { get; set; }

    [Column("Estado_Factura")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EstadoFactura { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("Facturacions")]
    public virtual Pedido? IdPedidoNavigation { get; set; }
}
