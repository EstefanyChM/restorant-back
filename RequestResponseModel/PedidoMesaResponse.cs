using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
    public class PedidoMesaResponse
    {
        public int? IdEnTienda { get; set; } 

        public int IdMesa { get; set; } 
        public int NroMesa { get; set; } 
        public bool? Finalizado { get; set; }  

        public bool Disponible { get; set; }  
        public int? IdPedido { get; set; }
        public decimal? Total { get; set; }

    }
}
