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
	public class ServiciosBussnies : IServiciosBussnies
	{
		#region Variables and Constructor / Dispose
		private readonly IServiciosRepository _serviciosRepository;
		private readonly IMapper _mapper;

		public ServiciosBussnies(IMapper mapper, IServiciosRepository serviciosRepository)
		{
			_mapper = mapper;
			_serviciosRepository = serviciosRepository;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		#endregion

		#region CRUD Methods
		public async Task<List<ServiciosResponse>> GetAll()
		{
			List<Service> servicios = await _serviciosRepository.GetAll();
			return _mapper.Map<List<ServiciosResponse>>(servicios);
		}

		public async Task<List<ServiciosResponse>> CreateMultiple(List<ServiciosRequest> lista)
		{
			List<Service> servicios = await _serviciosRepository.CreateMultiple(_mapper.Map<List<Service>>(lista));
			return _mapper.Map<List<ServiciosResponse>>(servicios);
		}

		public async Task<ServiciosResponse> Update(ServiciosRequest entity)
		{
			Service servicio = await _serviciosRepository.Update(_mapper.Map<Service>(entity));
			return _mapper.Map<ServiciosResponse>(servicio);
		}

		public async Task<List<ServiciosResponse>> UpdateMultiple(List<ServiciosRequest> lista)
		{
			List<Service> servicios = await _serviciosRepository.UpdateMultiple(_mapper.Map<List<Service>>(lista));
			return _mapper.Map<List<ServiciosResponse>>(servicios);
		}

		public async Task<int> Delete(int id)
		{
			return await _serviciosRepository.Delete(id);
		}

		public async Task<int> DeleteMultipleItems(List<ServiciosRequest> lista)
		{
			List<Service> servicios = _mapper.Map<List<Service>>(lista);
			return await _serviciosRepository.DeleteMultipleItems(servicios);
		}

		public async Task<GenericFilterResponse<ServiciosResponse>> GetByFilter(GenericFilterRequest request)
		{
			var result = _serviciosRepository.GetByFilter(request);
			return _mapper.Map<GenericFilterResponse<ServiciosResponse>>(result);
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _serviciosRepository.LogicDelete(id);
			return result;
		}

		public async Task<ServiciosResponse> Patch(int id, JsonPatchDocument<ServiciosRequest> patchDocument)
		{
			Service servicioDB = await _serviciosRepository.GetById(id);
			ServiciosRequest servicioDBRequest = _mapper.Map<ServiciosRequest>(servicioDB);
			patchDocument.ApplyTo(servicioDBRequest);
			_mapper.Map(servicioDBRequest, servicioDB);
			Service servicio = await _serviciosRepository.Update(servicioDB);
			ServiciosResponse result = _mapper.Map<ServiciosResponse>(servicio);
			return result;
		}

		public async Task<ServiciosResponse> GetById(int id)
		{
			Service servicio = await _serviciosRepository.GetById(id);

			/*if (!servicio.Estado)
			{
				if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
			}*/

			ServiciosResponse result = _mapper.Map<ServiciosResponse>(servicio);
			return result;
		}

		public async Task<ServiciosResponse> Create(ServiciosRequest entity)
		{
			Service servicio = _mapper.Map<Service>(entity);
			servicio = await _serviciosRepository.Create(servicio);
			ServiciosResponse result = _mapper.Map<ServiciosResponse>(servicio);
			return result;
		}

		#endregion
	}
}
