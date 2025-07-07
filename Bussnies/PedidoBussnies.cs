using AutoMapper;
using BDRiccosModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Repository;
using RequestResponseModel;
using System.Collections.Generic;

namespace Bussnies
{
	public class PedidoBussnies : IPedidoBussnies
	{
		/*INYECCIÓN DE DEPENDECIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		// private readonly IDetallePedidoRepository _DetallePedidoRepository;

		private readonly IPedidoRepository _PedidoRepository;
		private readonly IMapper _mapper;
		private readonly IProductoRepository _productoRepository;
		private readonly IUsuarioSistemaRepository _usuarioSistemaRepository;
		private readonly IPromocionRepository _promocionRepository;

		public PedidoBussnies(IMapper mapper,
			IProductoRepository productoRepository, IUsuarioSistemaRepository usuarioSistemaRepository,
			IPromocionRepository promocionRepository,
			IPedidoRepository pedidoRepository)
		{
			_mapper = mapper;
			_productoRepository = productoRepository;
			_usuarioSistemaRepository = usuarioSistemaRepository;
			_promocionRepository = promocionRepository;
			_PedidoRepository = pedidoRepository;
			//_DetallePedidoRepository = new DetallePedidoRepository();

		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<PedidoResponse>> GetAll()
		{
			List<Pedido> Pedidos = await _PedidoRepository.GetAll();
			List<PedidoResponse> lstResponse = _mapper.Map<List<PedidoResponse>>(Pedidos);
			return lstResponse;
		}

		public async Task<List<PedidoResponse>> GetAllCocina(bool PreparacionFinalizada)
		{
			List<Pedido> Pedidos = await _PedidoRepository.GeAllCocina(PreparacionFinalizada);
			List<PedidoResponse> lstResponse = _mapper.Map<List<PedidoResponse>>(Pedidos);
			return lstResponse;
		}

		public async Task<PedidoResponse> GetById(int id)
		{
			Pedido pedido = await _PedidoRepository.GetById(id);

			Pedido pedidoConNombreDelProducto = _PedidoRepository.ObtenerPedidoConNombreDelProducto(pedido.Id);
			PedidoResponse resul = _mapper.Map<PedidoResponse>(pedidoConNombreDelProducto);
			return resul;
		}

		public TimeSpan HoraPeru()
		{
			TimeZoneInfo peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
			DateTime peruTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, peruTimeZone);

			return peruTime.TimeOfDay;

		}

		public async Task<PedidoResponse> Create(PedidoRequest entity)
		{
			decimal totalFront = entity.Total;
			foreach (DetallePedidoRequest detallePedidoRequest in entity.DetallePedidosRequest)
			{
				detallePedidoRequest.EstadoPreparacion = false;
			}

			entity.HoraEntrada = HoraPeru();

			Pedido pedido = await _PedidoRepository.Create(_mapper.Map<Pedido>(entity));
			Pedido pedidoConNombreDelProducto = _PedidoRepository.ObtenerPedidoConNombreDelProducto(pedido.Id);

			// Iterar sobre cada DetallePedido del pedido para actualizar el stock de cada producto
			foreach (var detalle in pedidoConNombreDelProducto.DetallePedidos)
			{
				// Obtener el producto correspondiente al detalle
				Producto producto = await _productoRepository.GetById(detalle.IdProducto);

				if (producto.Stock >= detalle.Cantidad)
				{
					producto.Stock -= detalle.Cantidad;
					await _productoRepository.Update(producto);
				}
				else
				{
					throw new Exception($"No hay suficiente stock para el producto {producto.Nombre}.");
				}
			}

			PedidoResponse result = _mapper.Map<PedidoResponse>(pedidoConNombreDelProducto);

			if (totalFront != 0) result.Total = totalFront;

			return result;
		}


		public async Task<List<PedidoResponse>> CreateMultiple(List<PedidoRequest> lista)
		{
			List<Pedido> Pedidos = _mapper.Map<List<Pedido>>(lista);
			Pedidos = await _PedidoRepository.CreateMultiple(Pedidos);
			List<PedidoResponse> result = _mapper.Map<List<PedidoResponse>>(Pedidos);
			return result;
		}

		public async Task<PedidoResponse> Update(PedidoRequest entity)
		{
			var pedidoExistente = await _PedidoRepository.GetById(entity.Id);

			if (pedidoExistente == null)
			{
				throw new Exception("Pedido no encontrado");
			}

			// Desacoplar la entidad del contexto
			_PedidoRepository.Detach(pedidoExistente);

			decimal nuevoTotal = pedidoExistente.Total + entity.Total;

			Pedido pedidoActualizado = _mapper.Map<Pedido>(entity);
			pedidoActualizado.Total = nuevoTotal;

			Pedido pedido = await _PedidoRepository.Update(pedidoActualizado);
			PedidoResponse result = _mapper.Map<PedidoResponse>(pedido);



			return result;
		}

		public async Task<PedidoResponse> UpdateWithIdUser(PedidoRequest entity, int idUser)
		{
			entity.HoraEntrada = HoraPeru();

			var pedidoExistente = await _PedidoRepository.GetById(entity.Id);

			if (pedidoExistente == null) throw new Exception("Pedido no encontrado");
			// Desacoplar la entidad del contexto
			_PedidoRepository.Detach(pedidoExistente);


			decimal nuevoTotal = pedidoExistente.Total + entity.Total;
			/************************************/

			List<DetallesPromocion> detallesPedidosAAgregar = new List<DetallesPromocion>();
			List<DetallePedidoRequest> detallesPedidosAEliminar = new List<DetallePedidoRequest>();


			foreach (var detalle in entity.DetallePedidosRequest)
			{
				if (detalle.Id != 0)
				{
					detallesPedidosAEliminar.Add(detalle);

					var detallesPromocion = await _promocionRepository.obtenerLosDPDeLaPromo(detalle.Id);
					detallesPedidosAAgregar.AddRange(detallesPromocion);
				}


				detalle.IdUsuarioSistema = idUser;
			}

			entity.DetallePedidosRequest.RemoveAll(dp => detallesPedidosAEliminar.Contains(dp));

			List<DetallePedidoRequest> pre = _mapper.Map<List<DetallePedidoRequest>>(detallesPedidosAAgregar);

			entity.DetallePedidosRequest.AddRange(pre);
			/*********************************/


			foreach (var detalle in entity.DetallePedidosRequest)
			{
				detalle.IdUsuarioSistema = idUser;
				detalle.EstadoPreparacion = false;
			}

			Pedido pedidoActualizado = _mapper.Map<Pedido>(entity);
			pedidoActualizado.Total = nuevoTotal;

			Pedido pedido = await _PedidoRepository.Update(pedidoActualizado);
			PedidoResponse result = _mapper.Map<PedidoResponse>(pedido);

			return result;
		}



		public async Task<List<PedidoResponse>> UpdateMultiple(List<PedidoRequest> lista)
		{
			List<Pedido> Pedidos = _mapper.Map<List<Pedido>>(lista);
			Pedidos = await _PedidoRepository.UpdateMultiple(Pedidos);
			List<PedidoResponse> result = _mapper.Map<List<PedidoResponse>>(Pedidos);
			return result;
		}

		public async Task<int> Delete(int id)
		{
			int cantidad = await _PedidoRepository.Delete(id);
			return cantidad;
		}

		public async Task<int> DeleteMultipleItems(List<PedidoRequest> lista)
		{
			List<Pedido> Pedidos = _mapper.Map<List<Pedido>>(lista);
			int cantidad = await _PedidoRepository.DeleteMultipleItems(Pedidos);
			return cantidad;
		}

		public async Task<GenericFilterResponse<PedidoResponse>> GetByFilter(GenericFilterRequest request)
		{

			GenericFilterResponse<PedidoResponse> result = _mapper.Map<GenericFilterResponse<PedidoResponse>>(_PedidoRepository.GetByFilter(request));



			return result;
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _PedidoRepository.LogicDelete(id);
			return result;
		}

		public async Task<PedidoResponse> Patch(int id, JsonPatchDocument<PedidoRequest> patchDocument)
		{
			throw new NotImplementedException();
		}


		public async Task<List<PedidoDelUsuarioResponse>> GetPedidosMozoFiltrado(int idUser, int? idProducto, DateTime? fechaInicio, DateTime? fechaFin)
		{
			List<PedidoDelUsuarioResponse> pedidosPre = await _PedidoRepository.GetPedidosMozoFiltrado(idUser, idProducto, fechaInicio, fechaFin);

			return pedidosPre;
		}

		public async Task<DetallePedido> UpdateEstadoPreparacion(int idDetallePedido)
		{
			//DetallePedido detallePedido = await new CRUDRepository<DetallePedido>().GetById(idDetallePedido);

			DetallePedido detallePedido = await _PedidoRepository.GetByIdDetallePedido(idDetallePedido);

			detallePedido.EstadoPreparacion = true;

			//await new CRUDRepository<DetallePedido>().Update(detallePedido);
			await _PedidoRepository.UpdateDetallePedido(detallePedido);



			return _mapper.Map<DetallePedido>(detallePedido);
		}
		#endregion END CRUD METHODS
	}
}
