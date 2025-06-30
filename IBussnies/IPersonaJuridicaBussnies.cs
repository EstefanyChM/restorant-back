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
	public interface IPersonaJuridicaBussnies : ICRUDBussnies<PersonaJuridicaRequest, PersonaJuridicaResponse>
	{
		// List<VPersonaUbigeo> ObtenerTodosConUbigeo();
		PersonaJuridicaResponse GetByTipoNroDocumento(int tipoDocumento, string NroDocumento);
	}
}
