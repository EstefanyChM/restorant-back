using BDRiccosModel;
using CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IOnlineUserRepository:ICRUDRepository<OnlineUser>
    {
		Task<OnlineUser> ObtenerOnlineClient(string IdApplicationUser);

		Task<OnlineUser> GetByIdWithCustomeDetails(int id);

    }
}