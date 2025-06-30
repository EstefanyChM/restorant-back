using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IProductoRepository : ICRUDRepository<Producto>
	{
		//Task<GenericFilterResponse<Producto>> GetByFilteDependingRole(GenericFilterRequest request, string userRole);

		Task<List<Producto>> GetAllWithDetails();

		Task<List<Producto>> ObtenerProductosParaChatBot(string producto);

	}
}
