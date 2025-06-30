using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
    public class MesaRequest
    {

        public int Id { get; set; }

        public bool Estado { get; set; }

        public int Numero { get; set; }
        public bool Disponible { get; set; }
    }
}
