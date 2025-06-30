using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RequestResponseModel
{
    public class MetodoPagoRequest
	{
        public int IdMetodoPago { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido, bestia!")]
		[StringLength(maximumLength: 10, ErrorMessage = "Como {0} va a tener más de 10 letras,pe'! piensa")]
		//[StringLength(50)]
		public string Nombre { get; set; }
    }
}