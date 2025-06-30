using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface ICategoriaRepository : ICRUDRepository<Categoria>
	{
		Task<List<Categoria>> GetAllActive();
		Task<GenericFilterResponse<Categoria>> GetByFilteDependingRole(GenericFilterRequest request, string userRole);
	}
}
