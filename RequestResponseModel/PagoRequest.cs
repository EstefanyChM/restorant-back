using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BDRiccosModel;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel
{
	public class PagoRequest
    {
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        
        // Agregamos las propiedades que faltan
        public string NombreComprador { get; set; }
        public string EmailComprador { get; set; }
    }

}