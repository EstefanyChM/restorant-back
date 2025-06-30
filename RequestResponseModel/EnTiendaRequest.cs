using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class EnTiendaRequest
    {
        public int Id { get; set; }

    public int IdMesa { get; set; }
    public int IdPedido { get; set; }

        public PedidoRequest PedidoRequest { get; set; }

    }
}
