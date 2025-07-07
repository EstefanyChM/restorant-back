using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IPromocionRepository : ICRUDRepository<Promocion>
	{
		Task<List<DetallesPromocion>> obtenerLosDPDeLaPromo(int Id);

		Task<List<EmailSuscriptor>> obtenerSubscriptores();

	}
}
