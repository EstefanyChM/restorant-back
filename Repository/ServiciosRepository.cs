using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class ServiciosRepository : CRUDRepository<Service>, IServiciosRepository
	{
		public GenericFilterResponse<Service> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
