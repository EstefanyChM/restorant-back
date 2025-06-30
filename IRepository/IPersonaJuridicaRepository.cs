using BDRiccosModel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IPersonaJuridicaRepository : ICRUDRepository<PersonaJuridica>
	{
		//declarar los metodos personalizados
		//List<VPersonaUbigeo> ObtenerTodosConUbigeo(); PA CALMAR ESE ERROR
		//VPersona GetByTipoNroDocumento(string tipoDocumento, string NroDocumento);
		PersonaJuridica GetByTipoNroDocumento(int tipoDocumento, string NroDocumento);


	}
}
