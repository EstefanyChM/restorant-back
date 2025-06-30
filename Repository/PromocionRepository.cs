using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class PromocionRepository : CRUDRepository<Promocion>, IPromocionRepository
	{
		public GenericFilterResponse<Promocion> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<List<DetallesPromocion>> obtenerLosDPDeLaPromo(int Id)
		{
			List<DetallesPromocion> detallesPromocions = db.Set<DetallesPromocion>().Where(x => x.IdPromocion == Id).ToList();
			return detallesPromocions;
		}
	}
}
