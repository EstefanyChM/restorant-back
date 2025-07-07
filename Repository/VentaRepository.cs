using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class VentaRepository : CRUDRepository<Venta>, IVentaRepository
	{
		public VentaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public GenericFilterResponse<Venta> GetByFilter(GenericFilterRequest request)
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

						case "idMetodoPago":
							query = query.Where(x => x.IdMetodoPago == short.Parse(j.Value));
							break;
						case "fechaVenta":
							// Convertir la fecha recibida al tipo DateTime
							query = query.Where(x => x.FechaVenta.Date == DateTime.Parse(j.Value).Date);
							break;

							/*case "codigo":
								query = query.Where(x => x.Codigo.ToLower().Contains(j.Value.ToLower()));
								break;
							case "nombre":
								query = query.Where(x => x.Nombre.ToLower().Contains(j.Value.ToLower()));
								break;
							case "idEstado":
								query = query.Where(x => x.IdEstado == bool.Parse(j.Value));
								break;*/
					}
				}
			});

			GenericFilterResponse<Venta> res = new GenericFilterResponse<Venta>();

			res.TotalRegistros = query.Count();
			res.Lista = query
				//.Include(x => x.Status)
				.Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
				//.OrderBy(x => x.Nombre)
				.ToList();

			return res;
		}

		public async Task<List<Venta>> GetListaVentaOnlinePagada(int idService)
		{
			IQueryable<Venta> ventasYPedidos = dbSet
				//.Include(x => x.IdPedidoNavigation) //MEJOR SERÁ HACER EL LLAMADO POR GETBYID
				//.ThenInclude(x => x.DetallePedidos)
				//.Include(x => x.IdPedidoNavigation)
				//.ThenInclude(x=>x.Deliveries)
				.Where(x => x.IdEstadoVenta == 3 && x.IdPedidoNavigation.IdServicio == idService)// ==3 porque sólo los que el webhook ya modificó los datos, pero falta entregarlo de manera física
				;
			return await ventasYPedidos.ToListAsync();
		}


		public async Task<List<VistaDetallePedido>> GetListaVistaDetallePedido(int id)
		{
			var detasllePedido = db.VistaDetallePedidos
				.Where(x => x.IdVenta == id)
				.ToList();
			return detasllePedido;
			throw new NotImplementedException();
		}

		public async Task<Venta> GetVentaBoletaFactura(int id)
		{
			Venta venta = await dbSet
				.Include(x => x.IdPedidoNavigation.Pickups)
				.Include(x => x.IdPedidoNavigation.Deliveries)
				.Include(x => x.IdClienteNavigation.IdTablaPersonaNaturalNavigation)
				.Include(x => x.IdClienteNavigation.IdTablaPersonaJuridicaNavigation)
				.Include(x => x.IdMetodoPagoNavigation)
				.Include(x => x.IdPedidoNavigation.DetallePedidos)
				.ThenInclude(x => x.IdProductoNavigation)
				.FirstAsync(r => r.Id == id);
			return venta;
		}

		public async Task<Venta> GetDetallesCocina(int id)
		{
			Venta venta = await dbSet
				.Include(x => x.IdPedidoNavigation.Pickups)
				.Include(x => x.IdPedidoNavigation.Deliveries)
				.Include(x => x.IdPedidoNavigation.DetallePedidos)
				.ThenInclude(x => x.IdProductoNavigation)
				.FirstAsync(r => r.Id == id);
			return venta;
		}
		public async Task<VistaVenta> GetVistaVentaById(int id)
		{
			var venta = db.VistaVenta.Where(x => x.Id == id).FirstOrDefault();
			return venta;
		}


	}
}
