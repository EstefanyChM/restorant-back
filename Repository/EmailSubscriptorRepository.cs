using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;

namespace Repository
{
	public class EmailSuscriptorRepository : CRUDRepository<EmailSuscriptor>, IEmailSuscriptorRepository
	{
		public EmailSuscriptorRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public async Task<List<string>> ObtenerSuscriptoresActivos()
		{
			IQueryable<string> emailsActivos = dbSet
				.Where(s => s.Estado)
				.Select(s => s.Email);

			return await emailsActivos.ToListAsync();
		}


		public GenericFilterResponse<EmailSuscriptor> GetByFilter(GenericFilterRequest request)
		{
			IQueryable<EmailSuscriptor> query = dbSet;

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

			GenericFilterResponse<EmailSuscriptor> res = new GenericFilterResponse<EmailSuscriptor>();

			res.TotalRegistros = query.Count();

			res.Lista = query

				.Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
				.ToList();
			return res;
		}
	}
}
