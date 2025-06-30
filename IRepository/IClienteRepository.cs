using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IClienteRepository:ICRUDRepository<Cliente>
    {
		//GenericFilterResponse<VCliente> GetByFilterVCliente(GenericFilterRequest request);
		GenericFilterResponse<VCliente> GetByFilterVCliente(GenericFilterRequest request);

		VCliente GetByIdClienteVCliente(int idCliente);

		//int? GetIdPersona(int idCliente);

		int Update (object id);

		Cliente ObtenerClientePorIdPersona (int idPersona, int IdTipoPersona);



	}
}