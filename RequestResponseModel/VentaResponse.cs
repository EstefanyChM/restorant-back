using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class VentaResponse
    {
        public int Id { get; set; }

    public int IdCliente { get; set; }
    public DateTime FechaVenta { get; set; }
    public bool Estado { get; set; }

    public int IdPedido { get; set; }

    public int IdComprobante { get; set; }

    public int IdEstadoVenta { get; set; }

    public int IdMetodoPago { get; set; }

    public virtual PedidoResponse PedidoResponseNav { get; set; } = null!;

    }
}