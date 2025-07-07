using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class ProductoRepository : CRUDRepository<Producto>, IProductoRepository
	{
		public ProductoRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		/* public async Task<GenericFilterResponse<Producto>> GetByFilteDependingRole(GenericFilterRequest request, string userRole)
 {
	 decimal minPrice = Convert.ToDecimal(request.Filtros[0].Value);

	 decimal maxPrice =  Convert.ToDecimal(request.Filtros[1].Value);

	 IQueryable<Producto> query = userRole == "Administrador"
		 ? dbSet.Where(x => x.IdCategoriaNavigation.Estado == true && x.Precio >= minPrice && x.Precio <= maxPrice)
		 : dbSet.Where(x => x.Estado == true && x.IdCategoriaNavigation.Estado == true && x.Precio >=   minPrice   &&    x.Precio <= maxPrice);


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

				 case "disponibilidad":
					 query = query.Where(x => x.Disponibilidad == bool.Parse(j.Value));
					 break;
				 case "estado":
					 query = query.Where(x => x.Estado == bool.Parse(j.Value));
					 break;
				 case "idCategoria":
					 query = query.Where(x => x.IdCategoriaNavigation.Id == short.Parse(j.Value));
					 break;
			 }
		 }
	 });

	 GenericFilterResponse<Producto> res = new GenericFilterResponse<Producto>();

	 res.TotalRegistros = query.Count();

	 res.Lista = query
		 .Include(mc => mc.IdCategoriaNavigation)
		 .Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
		 .OrderBy(x => x.Nombre)
		 .ToList();

	 return res;
 }
 */

		public async Task<List<Producto>> GetAllWithDetails()
		{
			IQueryable<Producto> productos = dbSet
				.Include(x => x.IdCategoriaNavigation);
			return await productos.ToListAsync();
		}

		public GenericFilterResponse<Producto> GetByFilter(GenericFilterRequest request)
		{
			if (request.Filtros[2].Value == "") request.Filtros[2].Value = "true";

			decimal minPrice = Convert.ToDecimal(request.Filtros[0].Value);

			decimal maxPrice = Convert.ToDecimal(request.Filtros[1].Value);


			IQueryable<Producto> query = dbSet.Where(x => x.Precio >= minPrice && x.Precio <= maxPrice && x.IdCategoriaNavigation.Estado == true);

			//var query = dbSet.Where(x => x.Estado == true);

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

						case "idCategoria":
							query = query.Where(x => x.IdCategoria == short.Parse(j.Value));
							break;

						case "disponibilidad":
							query = query.Where(x => x.Disponibilidad == bool.Parse(j.Value));
							break;

						case "estado":
							query = query.Where(x => x.Estado == bool.Parse(j.Value));
							break;
					}
				}
			});

			GenericFilterResponse<Producto> res = new GenericFilterResponse<Producto>();

			res.TotalRegistros = query.Count();

			res.Lista = query
				.Include(mc => mc.IdCategoriaNavigation)
				.Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
				.ToList();

			return res;
		}


		public async Task<List<Producto>> ObtenerProductosParaChatBot(string producto)
		{
			List<Producto> productosObtenidos = await dbSet
				.Include(x => x.IdCategoriaNavigation)
				.Where(p => (p.Nombre.ToLower().Contains(producto.ToLower()) ||
							p.IdCategoriaNavigation.Nombre.ToLower().Contains(producto.ToLower())) &&
							p.Disponibilidad == true &&
							p.Estado == true &&
							p.Stock != 0)
				.ToListAsync();
			return productosObtenidos;
		}



	}
}
