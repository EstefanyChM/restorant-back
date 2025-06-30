using System.Collections.Generic;

namespace RequestResponseModel
{
    public class ItemCarritoResponse
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        // Otros campos que puedan ser necesarios
    }
}
