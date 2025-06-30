using BDRiccosModel;
using IRepository;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Repository
{
	public class ClienteRepository : CRUDRepository<Cliente>, IClienteRepository
	{
		

		public GenericFilterResponse<Cliente> GetByFilter(GenericFilterRequest request)
		{
			var query = dbSet.Where(x => x.Id == x.Id);

			/*request.Filtros.ForEach(j =>
			{
				if (!string.IsNullOrEmpty(j.Value))
				{
					switch (j.Name)
					{
						case "id":
							query = query.Where(x => x.IdCliente == short.Parse(j.Value));
							break;
						case "idPersona":
							query = query.Where(x => x.IdPersona == short.Parse(j.Value));
							break;
					}
				}
			});*/


			GenericFilterResponse<Cliente> res = new GenericFilterResponse<Cliente>();

			res.TotalRegistros = query.Count();
			res.Lista = query
				//.Include(x => x.Status)
				.Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
				//.OrderBy(x => x.Nombre)
				.ToList();

			return res;
		}



		/*public GenericFilterResponse<VCliente> GetByFilterVCliente(GenericFilterRequest request)
		{
			var query = db.VClientes.Where(x => x.IdCliente == x.IdCliente);

			request.Filtros.ForEach(j =>
			{
				if (!string.IsNullOrEmpty(j.Value))
				{
					switch (j.Name)
					{
						case "idCliente":
							query = query.Where(x => x.IdCliente == short.Parse(j.Value));
							break;
					}
				}
			});

			GenericFilterResponse<VCliente> res = new GenericFilterResponse<VCliente>();

			res.TotalRegistros = query.Count();

			res.Lista =query
					.Skip((request.NumeroPagina - 1) * request.Cantidad)
					//.Take(request.Cantidad)
				.OrderBy(x => x.Nombre)
				.ToList();

			return res;
		}*/



		public GenericFilterResponse<VCliente> GetByFilterVCliente(GenericFilterRequest request)//valores a filtrar
		{
			/*var query = db.VClientes.AsQueryable();

			if (!request.Filtros.Any(x => x.Name == "estado" && x.Value == "INACTIVO"))
			{
				query = query.Where(x => x.Estado == true);
			}*/


			/*request.Filtros.ForEach(j =>
			{
				//Si sí hay filtrado o 
				if (!string.IsNullOrEmpty(j.Value))
				{


					switch (j.Name)
					{
						case "idCliente":
							query = query.Where(x => x.IdCliente == short.Parse(j.Value));
							break;

						case "nroDocumento":
							query = query.Where(x => x.NroDocumento.ToLower().Contains(j.Value.ToLower()));
							break;

						case "nombre":
							query = query.Where(x => x.Nombre.ToLower().Contains(j.Value.ToLower()));
							break;

						case "estado":
							 //query = query.Where(x => x.Estado != null && x.Estado.ToLower().Contains(j.Value.ToLower()));


							Console.WriteLine($"Filtrando por estado: {j.Value.ToLower()}");
							break;
					}
				} 
			});



			GenericFilterResponse<VCliente> res = new GenericFilterResponse<VCliente>();

			res.TotalRegistros = query.Count();
			res.Lista = query
				.Skip((request.NumeroPagina - 1) * request.Cantidad)
				.Take(request.Cantidad)
				.OrderBy(x => x.Nombre)
				.ToList();

			return res;*/
			throw new NotImplementedException();
		}

		public VCliente GetByIdClienteVCliente(int idCliente)
		{
			// Realizar la consulta para obtener el primer VCliente con el idCliente especificado
			//VCliente vCliente = db.VClientes.FirstOrDefault(x => x.IdCliente == idCliente);

			// Retornar el resultado de la consulta
			//return vCliente;
			throw new NotImplementedException();
		}

		public Cliente ObtenerClientePorIdPersona(int IdPersona, int IdTipoPersona)
		{
			if (IdTipoPersona == 1)
			{ 
				Cliente cliente =dbSet.FirstOrDefault(x => x.IdTablaPersonaNatural == IdPersona && x.IdTipoPersona == IdTipoPersona);//Obtén al cliente con ese Id dePersona y el tipodePErsona, porque puede ser del otro tipo

			return cliente;
			}
			else
			{
				Cliente cliente = dbSet.FirstOrDefault(x => x.IdTablaPersonaJuridica == IdPersona && x.IdTipoPersona == IdTipoPersona);

			return cliente;
			}
		}

		public int Update(object id)
		{
            Cliente entityToDelete = dbSet.Find(id);
			entityToDelete.Estado = false;
			dbSet.Update(entityToDelete);
			return db.SaveChanges();
		}

	}



}
