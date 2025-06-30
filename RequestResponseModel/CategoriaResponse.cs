using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;

namespace RequestResponseModel
{
	public class CategoriaResponse
	{ 
		public int Id { get; set; }
		public string Nombre { get; set; }= "";
		public string Descripcion { get; set; }= "";

        public bool Disponibilidad { get; set; } = false;

		public string DisponibilidadDescripcion
        {
            get
            {
                return Disponibilidad? "Disponible":"No Disponible";
            }
        }

		public bool Estado { get;set;} = false;
		public string EstadoDescripcion
        {
            get
            {
                return Estado? "Activo":"Inactivo";
            }
        }
        public string UrlImagen { get; set; }

        public decimal PrecioMinimo { get; set; }

        public decimal PrecioMaximo { get; set; }
        public int CantidadDeProductos { get; set; }

	}

}
