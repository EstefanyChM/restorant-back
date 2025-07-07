using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
	public class EnTiendaRepository : CRUDRepository<EnTienda>, IEnTiendaRepository
	{
		public EnTiendaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public async Task<List<EnTienda>> GetAllFinalizado()
		{
			List<EnTienda> enTiendas = await dbSet
				.Include(x => x.IdPedidoNavigation)
				.Where(x => x.IdPedidoNavigation.Finalizado == true)
				.Where(x => x.IdPedidoNavigation.VentaFinalizada == false)
				.ToListAsync();
			;
			return enTiendas;
		}

		public GenericFilterResponse<EnTienda> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<List<PedidoMesaResponse>> GetPedidoMesa()
		{
			var result = from mesa in db.Mesas
						 join tienda in db.EnTiendas on mesa.Id equals tienda.IdMesa into tiendaGroup
						 from lastPedido in tiendaGroup
							 .OrderByDescending(t => t.Id)
							 .Take(1)
							 .DefaultIfEmpty()
						 where mesa.Estado == true
						 join pedido in db.Pedidos on lastPedido.IdPedido equals pedido.Id into pedidoGroup
						 from lastPedidoInfo in pedidoGroup.DefaultIfEmpty() // To handle cases where there is no order
						 select new PedidoMesaResponse
						 {
							 IdMesa = mesa.Id,
							 NroMesa = mesa.Numero,
							 Disponible = mesa.Disponible,
							 IdPedido = mesa.Disponible == false ? lastPedido.IdPedido : (int?)null,
							 Total = mesa.Disponible == false ? lastPedidoInfo.Total : (decimal?)null,
							 Finalizado = lastPedidoInfo.Finalizado,
							 IdEnTienda = lastPedido.Id
						 };

			return await result.ToListAsync();
		}

	}
}
