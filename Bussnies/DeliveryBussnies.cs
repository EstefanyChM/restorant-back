using AutoMapper;
using BDRiccosModel;
using ExcepcionesPersonalizadas;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository;
using RequestResponseModel;
using System.Collections.Generic;

namespace Bussnies
{
    public class DeliveryBussnies : IDeliveryBussnies
    {
        #region Variables and Constructor / Dispose
        private readonly IDeliveryRepository _deliveryRepository;
		private readonly IPedidoBussnies _pedidoBussnies;
		private readonly IPedidoRepository _pedidoRepository;
		private readonly IMesaRepository _mesaRepository;
		//private readonly IMapper _mapper;

        public DeliveryBussnies(IMapper mapper, IDeliveryRepository deliveryRepository, IPedidoBussnies pedidoBussnies,
            IPedidoRepository pedidoRepository,
            IMesaRepository mesaRepository)
        {
           // _mapper = mapper;
            _deliveryRepository = deliveryRepository;
			_pedidoBussnies = pedidoBussnies;
			_pedidoRepository = pedidoRepository;
			_mesaRepository = mesaRepository;
		}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region CRUD Methods
        public async Task<List<Delivery>> GetAll()
        {
            List<Delivery> deliveryList = await _deliveryRepository.GetAll();
            return deliveryList;
        }

       
        public async Task<List<Delivery>> CreateMultiple(List<Delivery> lista)
        {
            List<Delivery> deliveryList = await _deliveryRepository.CreateMultiple(lista);
            return deliveryList;
        }

        public async Task<Delivery> Update(Delivery entity)
        {
            Delivery delivery = await _deliveryRepository.Update(entity);
            return delivery;
        }

        public async Task<List<Delivery>> UpdateMultiple(List<Delivery> lista)
        {
            List<Delivery> deliveryList = await _deliveryRepository.UpdateMultiple(lista);
            return deliveryList;
        }

        public async Task<int> Delete(int id)
        {
            return await _deliveryRepository.Delete(id);
        }

        public async Task<int> DeleteMultipleItems(List<Delivery> lista)
        {
            List<Delivery> deliveryList =lista;
            return await _deliveryRepository.DeleteMultipleItems(deliveryList);
        }

        public async Task<GenericFilterResponse<Delivery>> GetByFilter(GenericFilterRequest filter)
        {
           //var result = await _deliveryRepository.GetByFilter(request);
           //return _mapper.Map<GenericFilterResponse<Delivery>>(result);
           throw new NotImplementedException();
        }

        public async Task<int> LogicDelete(int id)
        {
            var result = await _deliveryRepository.LogicDelete(id);
            return result;
        }

        public async Task<Delivery> GetById(int id)
        {
            Delivery delivery = await _deliveryRepository.GetById(id);

            /*if (!delivery.Estado)
            {
                if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
            }*/

            Delivery result = delivery;
            return result;
        }

        public async Task<Delivery> Create(Delivery entity)
        {
            //TENGO QUE HACER ESTO PORQUE EL PEDIDO YA SE HIZO AL CREARLOS PARA MERCADO PAGO

           // Pedido pedido =await  _pedidoRepository.GetById(entity.IdPedido);

            Delivery mandarDeliveryconPedidoYaHecho = new Delivery
            {
                Reference  = entity.Reference,
                Address = entity.Address,
                IdPedido = entity.IdPedido, 
            };

            Delivery delivery = await _deliveryRepository.Create(mandarDeliveryconPedidoYaHecho);
            return delivery;
        }

		public async Task<Delivery> FinalizarPedido(int id)
		{
            Delivery deliveryDB = await _deliveryRepository.GetById(id);

            Pedido pedidoDB = await _pedidoRepository.GetById(deliveryDB.IdPedido);
            pedidoDB.Finalizado = true;
            await _pedidoRepository.Update(pedidoDB);

            Mesa mesaDB = await _mesaRepository.GetById(deliveryDB.Id);
            mesaDB.Disponible = true;
            await _mesaRepository.Update(mesaDB);

            Delivery result = deliveryDB;
            return result;
		}

		public Task<Delivery> Patch(int id, JsonPatchDocument<Delivery> patchDocument)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
