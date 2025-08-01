﻿using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
	public class DeliveryRepository : CRUDRepository<Delivery>, IDeliveryRepository
	{
		public DeliveryRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public GenericFilterResponse<Delivery> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
