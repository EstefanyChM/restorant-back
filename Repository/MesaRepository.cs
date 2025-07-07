using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class MesaRepository : CRUDRepository<Mesa>, IMesaRepository
	{
		public MesaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public GenericFilterResponse<Mesa> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Mesa>> GetMesasEnTienda()
		{
			var a = db.Set<Mesa>().ToList();
			return a;
		}


	}
}
