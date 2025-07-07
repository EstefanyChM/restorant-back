using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class AsuntoMensajeRepository : CRUDRepository<Asunto>, IAsuntoMensajeRepository
	{
		public AsuntoMensajeRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public GenericFilterResponse<Asunto> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
