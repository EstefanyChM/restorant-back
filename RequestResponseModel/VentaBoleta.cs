using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;
public partial class VentaBoleta
{
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
    public decimal Total { get; set; }
    public DateTime FechaVenta { get; set; }


}
