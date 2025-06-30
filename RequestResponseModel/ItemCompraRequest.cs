using System;

namespace RequestResponseModel
{
    public class ItemCompraRequest
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        // Otros campos que puedan ser necesarios para cada ítem de compra
    }
}
