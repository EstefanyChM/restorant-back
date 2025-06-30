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
	public class AsuntoMensajeBussnies : IAsuntoMensajeBussnies
	{
		#region Variables and Constructor / Dispose
		private readonly IAsuntoMensajeRepository _asuntoMensajeRepository;
		private readonly IMapper _mapper;

		public AsuntoMensajeBussnies(IMapper mapper, IAsuntoMensajeRepository asuntoMensajeRepository)
		{
			_mapper = mapper;
			_asuntoMensajeRepository = asuntoMensajeRepository;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		#endregion

		#region CRUD Methods
		public async Task<List<AsuntoMensajeResponse>> GetAll()
		{
			List<Asunto> asuntos = await _asuntoMensajeRepository.GetAll();
			return _mapper.Map<List<AsuntoMensajeResponse>>(asuntos);
		}

		public async Task<List<AsuntoMensajeResponse>> CreateMultiple(List<AsuntoMensajeRequest> lista)
		{
			List<Asunto> asuntos = await _asuntoMensajeRepository.CreateMultiple(_mapper.Map<List<Asunto>>(lista));
			return _mapper.Map<List<AsuntoMensajeResponse>>(asuntos);
		}

		public async Task<AsuntoMensajeResponse> Update(AsuntoMensajeRequest entity)
		{
			Asunto asunto = await _asuntoMensajeRepository.Update(_mapper.Map<Asunto>(entity));
			return _mapper.Map<AsuntoMensajeResponse>(asunto);
		}

		public async Task<List<AsuntoMensajeResponse>> UpdateMultiple(List<AsuntoMensajeRequest> lista)
		{
			List<Asunto> asuntos = await _asuntoMensajeRepository.UpdateMultiple(_mapper.Map<List<Asunto>>(lista));
			return _mapper.Map<List<AsuntoMensajeResponse>>(asuntos);
		}

		public async Task<int> Delete(int id)
		{
			return await _asuntoMensajeRepository.Delete(id);
		}

		public async Task<int> DeleteMultipleItems(List<AsuntoMensajeRequest> lista)
		{
			List<Asunto> asuntos = _mapper.Map<List<Asunto>>(lista);
			return await _asuntoMensajeRepository.DeleteMultipleItems(asuntos);
		}

		public async Task<GenericFilterResponse<AsuntoMensajeResponse>> GetByFilter(GenericFilterRequest request)
		{
			var result = _asuntoMensajeRepository.GetByFilter(request);
			return _mapper.Map<GenericFilterResponse<AsuntoMensajeResponse>>(result);
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _asuntoMensajeRepository.LogicDelete(id);
			return result;
		}

		public async Task<AsuntoMensajeResponse> Patch(int id, JsonPatchDocument<AsuntoMensajeRequest> patchDocument)
		{
			Asunto asuntoDB = await _asuntoMensajeRepository.GetById(id);
			AsuntoMensajeRequest asuntoDBRequest = _mapper.Map<AsuntoMensajeRequest>(asuntoDB);
			patchDocument.ApplyTo(asuntoDBRequest);
			_mapper.Map(asuntoDBRequest, asuntoDB);
			Asunto asunto = await _asuntoMensajeRepository.Update(asuntoDB);
			AsuntoMensajeResponse result = _mapper.Map<AsuntoMensajeResponse>(asunto);
			return result;
		}

		public async Task<AsuntoMensajeResponse> GetById(int id)
		{
			Asunto asunto = await _asuntoMensajeRepository.GetById(id);

			/*if (!asunto.Estado)
			{
				if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
			}*/

			AsuntoMensajeResponse result = _mapper.Map<AsuntoMensajeResponse>(asunto);
			return result;
		}

		public async Task<AsuntoMensajeResponse> Create(AsuntoMensajeRequest entity)
		{
			Asunto asunto = _mapper.Map<Asunto>(entity);
			asunto = await _asuntoMensajeRepository.Create(asunto);
			AsuntoMensajeResponse result = _mapper.Map<AsuntoMensajeResponse>(asunto);
			return result;
		}

		#endregion
	}
}
