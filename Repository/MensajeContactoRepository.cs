using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Repository
{
	public class MensajeContactoRepository : CRUDRepository<MensajeContacto>, IMensajeContactoRepository
	{
        public async Task<List<MensajeContacto>> GetAllConDetalles()
        {
            IQueryable <MensajeContacto> mensajeContacto = dbSet
                .Include( x => x.IdAsuntoNavigation)
                .Include( x => x.IdEstadoMensajeNavigation)
                .Include( x => x.IdRemiteNavigation);

            return await mensajeContacto.ToListAsync();
        }
       
       
		public GenericFilterResponse<MensajeContacto> GetByFilter(GenericFilterRequest request)
        {
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
					}
				}
            });

            GenericFilterResponse<MensajeContacto> res = new GenericFilterResponse<MensajeContacto>();

            res.TotalRegistros = query.Count();
            res.Lista = query
                .Include( x => x.IdAsuntoNavigation)
                .Include( x => x.IdEstadoMensajeNavigation)
                .Include( x => x.IdRemiteNavigation)
                .Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
                //.OrderBy(x => x.Nombre)
                .ToList();

            return res;
        }
         /************************************/

		public Remite ObtenerRemitentePorId(int idRemitente)
        {
            return db.Set<Remite>().FirstOrDefault(r => r.Id == idRemitente);
        }

		public int Update(object id)
		{
			throw new NotImplementedException();
		}

		
	}


}
