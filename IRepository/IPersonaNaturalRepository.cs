using BDRiccosModel;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IPersonaNaturalRepository : ICRUDRepository<PersonaNatural>
	{
		//declarar los metodos personalizados
		//List<VPersonaUbigeo> ObtenerTodosConUbigeo(); PA CALMAR ESE ERROR
		//VPersona GetByTipoNroDocumento(string tipoDocumento, string NroDocumento);
		PersonaNatural GetByTipoNroDocumento(int tipoDocumento, string NroDocumento);


	}
}
