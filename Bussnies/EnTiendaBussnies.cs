using AutoMapper;
using BDRiccosModel;
using ExcepcionesPersonalizadas;
using IBussnies;
using IRepository;
using MercadoPago.Client.Preference;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository;
using RequestResponseModel;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace Bussnies
{
	public class EnTiendaBussnies : IEnTiendaBussnies
	{
		#region Variables and Constructor / Dispose
		private readonly IEnTiendaRepository _enTiendaRepository;
		private readonly IPedidoBussnies _pedidoBussnies;
		private readonly IPedidoRepository _pedidoRepository;
		private readonly IMesaRepository _mesaRepository;
		private readonly IUsuarioSistemaRepository _usuarioSistemaRepository;
		private readonly IPromocionRepository _promocionRepository;
		private readonly IMapper _mapper;

		public EnTiendaBussnies(IMapper mapper, IEnTiendaRepository enTiendaRepository, IPedidoBussnies pedidoBussnies,
			IPedidoRepository pedidoRepository,
			IMesaRepository mesaRepository,
			IUsuarioSistemaRepository usuarioSistemaRepository,
			IPromocionRepository promocionRepository)
		{
			_mapper = mapper;
			_enTiendaRepository = enTiendaRepository;
			_pedidoBussnies = pedidoBussnies;
			_pedidoRepository = pedidoRepository;
			_mesaRepository = mesaRepository;
			_usuarioSistemaRepository = usuarioSistemaRepository;
			_promocionRepository = promocionRepository;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		#endregion

		#region CRUD Methods
		public async Task<List<EnTiendaResponse>> GetAll()
		{
			List<EnTienda> enTiendaList = await _enTiendaRepository.GetAll();
			return _mapper.Map<List<EnTiendaResponse>>(enTiendaList);
		}

		public async Task<List<EnTiendaResponse>> GetAllFinalizado()
		{
			List<EnTienda> enTiendaList = await _enTiendaRepository.GetAllFinalizado();
			return _mapper.Map<List<EnTiendaResponse>>(enTiendaList);
		}

		public async Task<List<PedidoMesaResponse>> GetPedidoMesa()
		{
			List<PedidoMesaResponse> pedidosPorMesa = await _enTiendaRepository.GetPedidoMesa();
			return pedidosPorMesa;
		}

		public async Task<List<EnTiendaResponse>> CreateMultiple(List<EnTiendaRequest> lista)
		{
			List<EnTienda> enTiendaList = await _enTiendaRepository.CreateMultiple(_mapper.Map<List<EnTienda>>(lista));
			return _mapper.Map<List<EnTiendaResponse>>(enTiendaList);
		}

		public async Task<EnTiendaResponse> Update(EnTiendaRequest entity)
		{
			EnTienda enTienda = await _enTiendaRepository.Update(_mapper.Map<EnTienda>(entity));
			return _mapper.Map<EnTiendaResponse>(enTienda);
		}

		public async Task<List<EnTiendaResponse>> UpdateMultiple(List<EnTiendaRequest> lista)
		{
			List<EnTienda> enTiendaList = await _enTiendaRepository.UpdateMultiple(_mapper.Map<List<EnTienda>>(lista));
			return _mapper.Map<List<EnTiendaResponse>>(enTiendaList);
		}

		public async Task<int> Delete(int id)
		{
			return await _enTiendaRepository.Delete(id);
		}

		public async Task<int> DeleteMultipleItems(List<EnTiendaRequest> lista)
		{
			List<EnTienda> enTiendaList = _mapper.Map<List<EnTienda>>(lista);
			return await _enTiendaRepository.DeleteMultipleItems(enTiendaList);
		}

		public async Task<GenericFilterResponse<EnTiendaResponse>> GetByFilter(GenericFilterRequest request)
		{
			//var result = await _enTiendaRepository.GetByFilter(request);
			//return _mapper.Map<GenericFilterResponse<EnTiendaResponse>>(result);
			throw new NotImplementedException();
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _enTiendaRepository.LogicDelete(id);
			return result;
		}

		public async Task<EnTiendaResponse> GetById(int id)
		{
			EnTienda enTienda = await _enTiendaRepository.GetById(id);

			/*if (!enTienda.Estado)
            {
                if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
            }*/

			EnTiendaResponse result = _mapper.Map<EnTiendaResponse>(enTienda);
			return result;
		}

		public async Task<EnTiendaResponse> CrearConIdUsuario(int idUser, EnTiendaRequest entity)
		{
			decimal totalFront = entity.PedidoRequest.Total;

			Mesa mesaDB = await _mesaRepository.GetById(entity.IdMesa);
			mesaDB.Disponible = false;
			await _mesaRepository.Update(mesaDB);



			List<DetallesPromocion> detallesPedidosAAgregar = new List<DetallesPromocion>();
			List<DetallePedidoRequest> detallesPedidosAEliminar = new List<DetallePedidoRequest>();


			foreach (var detalle in entity.PedidoRequest.DetallePedidosRequest)
			{
				if (detalle.Id != 0)
				{
					detallesPedidosAEliminar.Add(detalle);

					var detallesPromocion = await _promocionRepository.obtenerLosDPDeLaPromo(detalle.Id);
					detallesPedidosAAgregar.AddRange(detallesPromocion);
				}


				detalle.IdUsuarioSistema = idUser;
			}

			entity.PedidoRequest.DetallePedidosRequest.RemoveAll(dp => detallesPedidosAEliminar.Contains(dp));

			List<DetallePedidoRequest> pre = _mapper.Map<List<DetallePedidoRequest>>(detallesPedidosAAgregar);

			foreach (var item in pre)
			{
				item.Id = 0;
				item.EstadoPreparacion = false;
				item.IdUsuarioSistema = idUser;
			}

			entity.PedidoRequest.DetallePedidosRequest.AddRange(pre);

			PedidoResponse pedido = await _pedidoBussnies.Create(entity.PedidoRequest);

			Pedido editar = await _pedidoRepository.GetById(pedido.Id);
			editar.Total = totalFront;

			await _pedidoRepository.Update(editar);





			EnTienda enTienda = _mapper.Map<EnTienda>(entity);
			enTienda.IdPedido = pedido.Id;
			enTienda = await _enTiendaRepository.Create(enTienda);
			EnTiendaResponse result = _mapper.Map<EnTiendaResponse>(enTienda);




			return result;
		}

		public async Task<EnTiendaResponse> FinalizarPedido(int id)
		{
			EnTienda enTiendaDB = await _enTiendaRepository.GetById(id);

			Pedido pedidoDB = await _pedidoRepository.GetById(enTiendaDB.IdPedido);
			pedidoDB.Finalizado = true;
			//Pedido pedidoEditado = 
			await _pedidoRepository.Update(pedidoDB);
			//PedidoResponse result = _mapper.Map<PedidoResponse>(pedidoEditado);

			Mesa mesaDB = await _mesaRepository.GetById(enTiendaDB.IdMesa);
			mesaDB.Disponible = true;
			await _mesaRepository.Update(mesaDB);

			EnTiendaResponse result = _mapper.Map<EnTiendaResponse>(enTiendaDB);



			return result;
		}

		public Task<EnTiendaResponse> Patch(int id, JsonPatchDocument<EnTiendaRequest> patchDocument)
		{
			throw new NotImplementedException();
		}

		public Task<EnTiendaResponse> Create(EnTiendaRequest entity)
		{
			throw new NotImplementedException();
		}



		#endregion
	}
}
