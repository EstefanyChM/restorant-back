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
    public class PickupBussnies : IPickupBussnies
    {
        #region Variables and Constructor / Dispose
        private readonly IPickupRepository _pickupRepository;
		private readonly IPedidoBussnies _pedidoBussnies;
		private readonly IPedidoRepository _pedidoRepository;
		private readonly IMesaRepository _mesaRepository;
		//private readonly IMapper _mapper;

        public PickupBussnies(IMapper mapper, IPickupRepository pickupRepository, IPedidoBussnies pedidoBussnies,
            IPedidoRepository pedidoRepository,
            IMesaRepository mesaRepository)
        {
           // _mapper = mapper;
            _pickupRepository = pickupRepository;
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
        public async Task<List<Pickup>> GetAll()
        {
            List<Pickup> pickupList = await _pickupRepository.GetAll();
            return pickupList;
        }

        public async Task<List<Pickup>> CreateMultiple(List<Pickup> lista)
        {
            List<Pickup> pickupList = await _pickupRepository.CreateMultiple(lista);
            return pickupList;
        }

        public async Task<Pickup> Update(Pickup entity)
        {
            Pickup pickup = await _pickupRepository.Update(entity);
            return pickup;
        }

        public async Task<List<Pickup>> UpdateMultiple(List<Pickup> lista)
        {
            List<Pickup> pickupList = await _pickupRepository.UpdateMultiple(lista);
            return pickupList;
        }

        public async Task<int> Delete(int id)
        {
            return await _pickupRepository.Delete(id);
        }

        public async Task<int> DeleteMultipleItems(List<Pickup> lista)
        {
            List<Pickup> pickupList = lista;
            return await _pickupRepository.DeleteMultipleItems(pickupList);
        }

        public async Task<GenericFilterResponse<Pickup>> GetByFilter(GenericFilterRequest filter)
        {
           //var result = await _pickupRepository.GetByFilter(request);
           //return _mapper.Map<GenericFilterResponse<Pickup>>(result);
           throw new NotImplementedException();
        }

        public async Task<int> LogicDelete(int id)
        {
            var result = await _pickupRepository.LogicDelete(id);
            return result;
        }

        public async Task<Pickup> GetById(int id)
        {
            Pickup pickup = await _pickupRepository.GetById(id);

            /*if (!pickup.Estado)
            {
                if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
            }*/

            Pickup result = pickup;
            return result;
        }

        public async Task<Pickup> Create(Pickup entity)
        {
            Pickup mandarPickupconPedidoYaHecho = new Pickup
            {
                PickupTime = entity.PickupTime,
                IdPedido = entity.IdPedido, 
            };
            
            Pickup pickup = await _pickupRepository.Create(mandarPickupconPedidoYaHecho);
            return pickup;

        }

		public async Task<Pickup> FinalizarPedido(int id)
		{
            Pickup pickupDB = await _pickupRepository.GetById(id);

            Pedido pedidoDB = await _pedidoRepository.GetById(pickupDB.IdPedido);
            pedidoDB.Finalizado = true;
            await _pedidoRepository.Update(pedidoDB);

            Mesa mesaDB = await _mesaRepository.GetById(pickupDB.Id);
            mesaDB.Disponible = true;
            await _mesaRepository.Update(mesaDB);

            Pickup result = pickupDB;
            return result;
		}

		public Task<Pickup> Patch(int id, JsonPatchDocument<Pickup> patchDocument)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
