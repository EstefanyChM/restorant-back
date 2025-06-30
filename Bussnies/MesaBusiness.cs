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
    public class MesaBusiness : IMesaBussnies
    {
        #region Variables and Constructor / Dispose
        private readonly IMesaRepository _mesaRepository;
        private readonly IMapper _mapper;

        public MesaBusiness(IMapper mapper, IMesaRepository mesaRepository)
        {
            _mapper = mapper;
            _mesaRepository = mesaRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region CRUD Methods
        public async Task<List<MesaResponse>> GetAll()
        {
            List<Mesa> mesas = await _mesaRepository.GetAll();
            return _mapper.Map<List<MesaResponse>>(mesas);
        }

        public async Task<List<MesaResponse>> GetMesasEnTienda()
        {
            List<Mesa> mesas = await _mesaRepository.GetMesasEnTienda();
            return _mapper.Map<List<MesaResponse>>(mesas);
        }



        public async Task<List<MesaResponse>> CreateMultiple(List<MesaRequest> lista)
        {
            List<Mesa> mesas = await _mesaRepository.CreateMultiple(_mapper.Map<List<Mesa>>(lista));
            return _mapper.Map<List<MesaResponse>>(mesas);
        }

        public async Task<MesaResponse> Update(MesaRequest entity)
        {
            Mesa mesa = await _mesaRepository.Update(_mapper.Map<Mesa>(entity));
            return _mapper.Map<MesaResponse>(mesa);
        }

        public async Task<List<MesaResponse>> UpdateMultiple(List<MesaRequest> lista)
        {
            List<Mesa> mesas = await _mesaRepository.UpdateMultiple(_mapper.Map<List<Mesa>>(lista));
            return _mapper.Map<List<MesaResponse>>(mesas);
        }

        public async Task<int> Delete(int id)
        {
            return await _mesaRepository.Delete(id);
        }

        public async Task<int> DeleteMultipleItems(List<MesaRequest> lista)
        {
            List<Mesa> mesas = _mapper.Map<List<Mesa>>(lista);
            return await _mesaRepository.DeleteMultipleItems(mesas);
        }

        public async Task<GenericFilterResponse<MesaResponse>> GetByFilter(GenericFilterRequest request)
        {
            var result = _mesaRepository.GetByFilter(request);
            return _mapper.Map<GenericFilterResponse<MesaResponse>>(result);
        }

        public async Task<int> LogicDelete(int id)
        {
            var result = await _mesaRepository.LogicDelete(id);
            return result;
        }

        public async Task<MesaResponse> Patch(int id, JsonPatchDocument<MesaRequest> patchDocument)
        {
            Mesa mesaDB = await _mesaRepository.GetById(id);
            MesaRequest mesaDBRequest = _mapper.Map<MesaRequest>(mesaDB);
            patchDocument.ApplyTo(mesaDBRequest);
            _mapper.Map(mesaDBRequest, mesaDB);
            Mesa mesa = await _mesaRepository.Update(mesaDB);
            MesaResponse result = _mapper.Map<MesaResponse>(mesa);
            return result;
        }

        public async Task<MesaResponse> GetById(int id, string userRole)
        {
            Mesa mesa = await _mesaRepository.GetById(id);

            if (!mesa.Estado)
            {
                if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
            }

            MesaResponse result = _mapper.Map<MesaResponse>(mesa);
            return result;
        }

        public async Task<MesaResponse> Create(MesaRequest entity)
        {
            Mesa mesa = _mapper.Map<Mesa>(entity);
            mesa = await _mesaRepository.Create(mesa);
            MesaResponse result = _mapper.Map<MesaResponse>(mesa);
            return result;
        }

        public Task<MesaResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
