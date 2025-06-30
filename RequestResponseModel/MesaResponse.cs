using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
    public class MesaResponse
    {
        
        public int Id { get; set; }

        public bool Estado { get; set; }

        public int Numero { get; set; }
        public bool Disponible { get; set; }
    

    }
}