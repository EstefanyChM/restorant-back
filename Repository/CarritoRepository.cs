using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
    public class CarritoRepository : CRUDRepository<Carrito>, ICarritoRepository
    {
        public GenericFilterResponse<Carrito> GetByFilter(GenericFilterRequest request)
        {
            //if (request.Filtros[1].Value == "") request.Filtros[1].Value = "true";

           var query = dbSet.Where(x => x.Id == x.Id);

            request.Filtros.ForEach(j =>
            {
                if (!string.IsNullOrEmpty(j.Value))
                {
                    switch (j.Name)
                    {
                        case "id":
                            query = query.Where(x => x.Id == short.Parse(j.Value));
                            break;

                       /* case "nombre":
                            query = query.Where(x => x.Nombre.ToLower().Contains(j.Value.ToLower()));
                            break;

                        case "disponibilidad":
                            query = query.Where(x => x.Disponibilidad == bool.Parse(j.Value));
                            break;

                        case "estado":
                            query = query.Where(x => x.Estado == bool.Parse(j.Value));
                            break;*/
                    }
                }
            });

            GenericFilterResponse<Carrito> res = new GenericFilterResponse<Carrito>();

            res.TotalRegistros = query.Count();

            res.Lista = query
                //.Include(mc => mc.IdCategoriaNavigation)
                .Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
                .ToList();

            return res;
        }
    }
}
