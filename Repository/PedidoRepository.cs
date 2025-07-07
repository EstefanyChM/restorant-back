using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class PedidoRepository : CRUDRepository<Pedido>, IPedidoRepository
	{
		public PedidoRepository(_dbRiccosContext context) : base(context)
		{
		}



		public Task<DetallePedido> UpdateDetallePedido(DetallePedido detallePedido)
		{
			throw new NotImplementedException();
		}

		public Task<DetallePedido> GetByIdDetallePedido(int idDetallePedido)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Pedido>> GeAllCocina(bool PreparacionFinalizada)
		{
			List<Pedido> pedidos = await dbSet
				.Where(x => x.Finalizado == false
							&& x.VentaFinalizada == false
							&& x.DetallePedidos.Any(dp => dp.EstadoPreparacion == false) // Filtrar pedidos con al menos un detalle que cumpla
							)
				.Include(x => x.DetallePedidos)
					.ThenInclude(dp => dp.IdProductoNavigation)
				.OrderBy(x => x.HoraEntrada)
				.ToListAsync();

			// Filtrar DetallePedidos en memoria
			foreach (var pedido in pedidos)
			{
				pedido.DetallePedidos = pedido.DetallePedidos
					.Where(dp => dp.EstadoPreparacion == PreparacionFinalizada)
					.ToList();
			}

			return pedidos;
		}



		public GenericFilterResponse<Pedido> GetByFilter(GenericFilterRequest request)
		{
			var query = dbSet.Where(x => x.Estado);

			request.Filtros.ForEach(j =>
			{
				if (!string.IsNullOrEmpty(j.Value))
				{
					switch (j.Name)
					{
						case "id":
							query = query.Where(x => x.Id == short.Parse(j.Value));
							break;
						case "estadoFinalizado":
							query = query.Where(x => x.Finalizado == bool.Parse(j.Value));
							break;
						case "estadoVentaFinalizada":
							query = query.Where(x => x.VentaFinalizada == bool.Parse(j.Value));
							break;
						case "servicio":
							query = query.Where(x => x.IdServicio == int.Parse(j.Value));
							break;
						/* case "codigo":
							 query = query.Where(x => x.Codigo.ToLower().Contains(j.Value.ToLower()));
							 break;
						 case "nombre":
							 query = query.Where(x => x.Nombre.ToLower().Contains(j.Value.ToLower()));
							 break;*/
						case "estado":
							query = query.Where(x => x.Estado == bool.Parse(j.Value));
							break;
					}
				}
			});

			GenericFilterResponse<Pedido> res = new GenericFilterResponse<Pedido>();

			res.TotalRegistros = query.Count();
			res.Lista = query
				.Include(x => x.DetallePedidos)
				.ThenInclude(x => x.IdProductoNavigation)
				.ThenInclude(x => x.IdCategoriaNavigation)
				.Skip((request.NumeroPagina - 1) * request.Cantidad).Take(request.Cantidad)
				.OrderBy(x => x.HoraEntrada)
				.ToList();

			return res;
		}




		/*public async Task<List<Pedido>> GetPedidosMozoFiltrado(int idUser, int? idProducto, DateTime? fechaInicio, DateTime? fechaFin)
		{
			List<Pedido> pedidos = await dbSet
				.Where(p => //p.DetallePedidos.Any(dp => dp.IdUsuarioSistema == idUser) &&
						   (!idProducto.HasValue || p.DetallePedidos.Any(dp => dp.IdProducto == idProducto)) &&
						   (!fechaInicio.HasValue || !fechaFin.HasValue || p.Venta.Any(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)))
				.ToListAsync();

			return pedidos;
		}*/



		public async Task<List<PedidoDelUsuarioResponse>> GetPedidosMozoFiltrado(int idUser, int? idProducto, DateTime? fechaInicio, DateTime? fechaFin)
		{
			/*select  *, T_DP.Id idDP , T_V.Id idV from [pedido].[DetallePedido] T_DP
join [venta].[Venta] T_V on T_DP.IdPedido = T_V.IdPedido where T_DP.IdUsuarioSistema=6;*/


			TimeZoneInfo peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
			DateTime nowInPeru = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);

			// Si fechaInicio o fechaFin son null, usar la fecha actual del servidor en Perú
			fechaInicio ??= nowInPeru.Date; // Inicio del día actual
			fechaFin ??= nowInPeru.Date.AddDays(1).AddSeconds(-1); // Fin del día actual


			var query = from dp in db.DetallePedidos
						join v in db.Ventas on dp.IdPedido equals v.IdPedido
						join pro in db.Productos on dp.IdProducto equals pro.Id
						where dp.IdUsuarioSistema == idUser
						&& (!idProducto.HasValue || dp.IdProducto == idProducto)
						&& (!fechaInicio.HasValue || !fechaFin.HasValue || (v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin))
						select new PedidoDelUsuarioResponse
						{
							Fecha = v.FechaVenta,
							IdProducto = dp.IdProducto,
							NombreProducto = pro.Nombre,
							Cantidad = dp.Cantidad
						};

			var response = await query.ToListAsync();
			return response;
		}

		public Pedido ObtenerPedidoConNombreDelProducto(int id)
		{
			var query = dbSet.Include(x => x.DetallePedidos)
							  .ThenInclude(x => x.IdProductoNavigation)
							  .ThenInclude(x => x.IdCategoriaNavigation)
							  .FirstOrDefault(x => x.Id == id);

			return query;
		}

	}
}
