

namespace RequestResponseModel
{
	public class ClienteResponse
	{
		public int Id { get; set; }

        public int? IdTablaPersonaJuridica { get; set; }

        public int? IdTablaPersonaNatural { get; set; }

        public bool Estado { get; set; }

        public short IdTipoPersona { get; set; }


	}
}