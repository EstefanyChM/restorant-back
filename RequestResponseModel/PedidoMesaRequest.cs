using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class PedidoMesaRequest
    {
        public int IdMesa { get; set; }        // ID de la mesa
        public int NroMesa { get; set; }       // Número de la mesa
        public bool Disponible { get; set; }   // Estado de     disponibilidad
        public int? IdPedido { get; set; }     // ID del último pedido (null si no hay pedido o si la mesa está disponible)
    }
}
