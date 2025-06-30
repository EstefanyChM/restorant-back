using BDRiccosModel;
using CommonModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IClienteRegistradoRepository : ICRUDRepository<OnlineUser>
	{
		List<OnlineUser> GetAllActive();
	}
}
