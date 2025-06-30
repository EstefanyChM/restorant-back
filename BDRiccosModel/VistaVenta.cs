using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Keyless]
public partial class VistaVenta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Apellidos { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string NumeroDocumento { get; set; } = null!;

    public int IdPedido { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaVenta { get; set; }

    public bool Estado { get; set; }

    public int IdComprobante { get; set; }

    public int IdPago { get; set; }

    public int IdEstadoVenta { get; set; }
}
