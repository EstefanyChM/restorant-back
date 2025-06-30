using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class VentaRequest
    {
        public int Id { get; set; }

    public int IdCliente { get; set; }
    public DateTime FechaVenta { get; set; }
    public bool Estado { get; set; }

    public int IdPedido { get; set; }

    public int IdComprobante { get; set; }

    public int IdEstadoVenta { get; set; }

    public int IdMetodoPago { get; set; }
    }
}