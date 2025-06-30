using System.Collections.Generic;

namespace RequestResponseModel
{
    public class CarritoResponse
    {
        public int UsuarioId { get; set; }
        public List<ItemCarritoResponse> Items { get; set; }
        // Otros campos que puedan ser necesarios
    }
}
