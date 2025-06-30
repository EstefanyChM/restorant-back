using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;

namespace RequestResponseModel
{
    public class CarritoRequest
    {
        public int Id { get; set; }
        public int IdCliente { get; set; } // Identificador del cliente
        public List<DetallePedido> Detalles { get; set; } // Lista de items en el carrito
        public decimal TotalPagar { get; set; } // Total a pagar por el carrito
        public bool Estado { get; set; } // Estado del carrito (activo, finalizado, etc.)

        public int IdTipoEntrega { get; set; } //Delivery, en tienda o si presenciadasñdj
    }

}
