using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class HorarioAtencionRepository : CRUDRepository<HorariosRegulares>, IHorarioAtencionRepository
	{
		GenericFilterResponse<HorariosRegulares> ICRUDRepository<HorariosRegulares>.GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
