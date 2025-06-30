using BDRiccosModel;
using IRepository;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class MetodoPagoRepository : CRUDRepository<MetodoPago>, IMetodoPagoRepository
	{
		public GenericFilterResponse<MetodoPago> GetByFilter(GenericFilterRequest request)
		{
			var query = dbSet.Where(x => x.Id == x.Id);


			if (request.Filtros.Any(j => j.Name == "nombre"))
			{
				var nombreFilter = request.Filtros.First(j => j.Name == "nombre").Value.ToLower();
				query = query.Where(x => x.Nombre.ToLower().Contains(nombreFilter));
			}


			request.Filtros.ForEach(j =>
			{
				if (!string.IsNullOrEmpty(j.Value))
				{
					switch (j.Name)
					{
						case "id":
							query = query.Where(x => x.Id == short.Parse(j.Value));
							break;
						case "nombre":
							query = query.Where(x => x.Nombre.ToLower().Contains(j.Value.ToLower()));
							break;
					}
				}
			});

			GenericFilterResponse<MetodoPago> res = new GenericFilterResponse<MetodoPago>();

			res.TotalRegistros = query.Count();
			res.Lista = query
				.Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
				.OrderBy(x => x.Nombre)
				.ToList();

			return res;
		}
	}
}

