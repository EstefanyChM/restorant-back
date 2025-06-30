using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPersonalizadas
{
	
	[Serializable]
	public class DatoDuplicadoException : Exception
	{
		public DatoDuplicadoException()
		{
		}

		public DatoDuplicadoException(string? message) : base(message)
		{
		}

		public DatoDuplicadoException(string entidad, string? message, Exception? innerException) : base(message, innerException)
		{
		}

		protected DatoDuplicadoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
