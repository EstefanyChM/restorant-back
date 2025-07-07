using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class EmpresaRepository : CRUDRepository<Empresa>, IEmpresaRepository
	{
		public EmpresaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public GenericFilterResponse<Empresa> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Empresa>> GetAllWithSchedules()
		{
			List<Empresa> empresas = await dbSet.Include(h => h.Horarios).ToListAsync();
			return empresas;
		}
	}
}