using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class OnlineUserRepository : CRUDRepository<OnlineUser>, IOnlineUserRepository
	{
		public GenericFilterResponse<OnlineUser> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<OnlineUser> GetByIdWithCustomeDetails(int id)
		{
			OnlineUser onlineUser = await dbSet
				.Include(x => x.IdClienteNavigation)
				.ThenInclude( x => x.IdTablaPersonaNaturalNavigation)
				.FirstOrDefaultAsync(x => x.Id == id)
				;
			return onlineUser;
		}

		public async Task<OnlineUser> ObtenerOnlineClient(string IdApplicationUser)
		{
			OnlineUser onlineUser = await dbSet
                .FirstOrDefaultAsync(ou => ou.IdApplicationUser == IdApplicationUser);
			return onlineUser;
		}
	}
}
