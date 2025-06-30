using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
    public class EnTiendaResponse
    {
        public int Id { get; set; }
        public int IdMesa { get; set; }
        public int IdPedido { get; set; }
        public bool Finalizado { get; set; }
        public decimal Total { get; set; }
    }
}
