using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class CategoriaRepository : CRUDRepository<Categoria>, ICategoriaRepository
    {


		public async Task<List<Categoria>> GetAllActive()
		{
			IQueryable <Categoria> categoriasActivas= dbSet
                .Include(productos => productos.Productos)
                .Where(p => p.Estado == bool.Parse("true") && p.Disponibilidad == bool.Parse("true") );

            return await categoriasActivas.ToListAsync();
		}
        
		public async Task<GenericFilterResponse<Categoria>> GetByFilteDependingRole(GenericFilterRequest request, string userRole)
        {
            IQueryable<Categoria> query = userRole == "Administrador"
                ? dbSet
                :dbSet.Where(x => x.Estado == true) ;

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
                    }
                }
            });

            GenericFilterResponse<Categoria> res = new GenericFilterResponse<Categoria>();

            res.TotalRegistros = query.Count();

            res.Lista = query
                .Include(productos => productos.Productos)
                .Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
                .OrderBy(x => x.Nombre)
                .ToList();

            return res;
        }


		public GenericFilterResponse<Categoria> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		
	}
}
