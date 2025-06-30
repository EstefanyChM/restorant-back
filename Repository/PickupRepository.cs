using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class PickupRepository : CRUDRepository<Pickup>, IPickupRepository
    {
        public GenericFilterResponse<Pickup> GetByFilter(GenericFilterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
