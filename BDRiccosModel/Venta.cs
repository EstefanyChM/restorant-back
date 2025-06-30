using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Venta", Schema = "venta")]
[Index("IdComprobante", Name = "IX_Venta_id_comprobante")]
[Index("IdPedido", Name = "IX_Venta_id_pedido")]
public partial class Venta
{
    [Key]
    public int Id { get; set; }

    public int IdCliente { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaVenta { get; set; }

    public bool Estado { get; set; }

    public int IdPedido { get; set; }

    public int? IdVentaMercadoPago { get; set; }
    public int IdComprobante { get; set; }

    public int IdEstadoVenta { get; set; }

    public int IdMetodoPago { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Venta")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdEstadoVenta")]
    [InverseProperty("Venta")]
    public virtual EstadoVenta IdEstadoVentaNavigation { get; set; } = null!;

    [ForeignKey("IdMetodoPago")]
    [InverseProperty("Venta")]
    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    

    [ForeignKey("IdPedido")]
    [InverseProperty("Venta")]
    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
