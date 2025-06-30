using System;

namespace RequestResponseModel
{
    public class CompraRequest
    {
        public int UsuarioId { get; set; }
        public List<ItemCompraRequest> Items { get; set; }
        // Otros campos que puedan ser necesarios para la compra
    }
}
