using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RequestResponseModel
{
    public class ClienteRequest
    {
		public int Id { get; set; }

        public int? IdTablaPersonaJuridica { get; set; }

        public int? IdTablaPersonaNatural { get; set; }

        public bool Estado { get; set; }

        public short IdTipoPersona { get; set; }
	}
}


