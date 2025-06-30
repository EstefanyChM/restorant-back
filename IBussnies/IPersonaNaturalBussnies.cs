using BDRiccosModel;
using RequestResponseModel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBussnies
{
	public interface IPersonaNaturalBussnies : ICRUDBussnies<PersonaNaturalRequest, PersonaNaturalResponse>
	{
		// List<VPersonaUbigeo> ObtenerTodosConUbigeo();
		PersonaNaturalResponse GetByTipoNroDocumento(int tipoDocumento, string NroDocumento);
	}
}
