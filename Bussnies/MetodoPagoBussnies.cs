using AutoMapper;
using BDRiccosModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Repository;
using RequestResponseModel;
using System.Collections.Generic;

namespace Bussnies
{
	public class MetodoPagoBussnies : IMetodoPagoBussnies
	{
		/*INYECCIÓN DE DEPENDECIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		private readonly IMetodoPagoRepository _MetodoPagoRepository;
		private readonly IMapper _mapper;
		public MetodoPagoBussnies(IMapper mapper)
		{
			_mapper = mapper;
			_MetodoPagoRepository = new MetodoPagoRepository();
		}


		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<MetodoPagoResponse>> GetAll()
		{
			List<MetodoPago> MetodoPagos = await _MetodoPagoRepository.GetAll();
			List<MetodoPagoResponse> lstResponse = _mapper.Map<List<MetodoPagoResponse>>(MetodoPagos);
			return lstResponse;
		}



		public async Task<MetodoPagoResponse> GetById(int id)
		{
			MetodoPago MetodoPago =await _MetodoPagoRepository.GetById(id);
			MetodoPagoResponse resul = _mapper.Map<MetodoPagoResponse>(MetodoPago);
			return resul;
		}

		public async Task<MetodoPagoResponse> Create(MetodoPagoRequest entity)
		{
			var yaExiste = _MetodoPagoRepository.GetAllQueryable().Any(x => x.Nombre == entity.Nombre);

			if (yaExiste)
			{
				throw new InvalidOperationException("Ya existe un Tipo de Habitación con el mismo nombreee.");
				//return new MetodoPagoResponse { IdMetodoPago = -1, Nombre = "Ya existe un Tipo de Habitación con el mismo nombre." };
			}

			MetodoPago MetodoPago = _mapper.Map<MetodoPago>(entity);


			MetodoPago =await _MetodoPagoRepository.Create(MetodoPago);
			MetodoPagoResponse result = _mapper.Map<MetodoPagoResponse>(MetodoPago);
			return result;
		}
		public async Task<List<MetodoPagoResponse>> CreateMultiple(List<MetodoPagoRequest> lista)
		{
			List<MetodoPago> MetodoPagos = _mapper.Map<List<MetodoPago>>(lista);
			MetodoPagos = await _MetodoPagoRepository.CreateMultiple(MetodoPagos);
			List<MetodoPagoResponse> result = _mapper.Map<List<MetodoPagoResponse>>(MetodoPagos);
			return result;
		}

		public async Task<MetodoPagoResponse> Update(MetodoPagoRequest entity)
		{
			MetodoPago MetodoPago = _mapper.Map<MetodoPago>(entity);
			MetodoPago =await _MetodoPagoRepository.Update(MetodoPago);
			MetodoPagoResponse result = _mapper.Map<MetodoPagoResponse>(MetodoPago);
			return result;
		}

		public async Task<List<MetodoPagoResponse>> UpdateMultiple(List<MetodoPagoRequest> lista)
		{
			List<MetodoPago> MetodoPagos = _mapper.Map<List<MetodoPago>>(lista);
			MetodoPagos = await _MetodoPagoRepository.UpdateMultiple(MetodoPagos);
			List<MetodoPagoResponse> result = _mapper.Map<List<MetodoPagoResponse>>(MetodoPagos);
			return result;
		}

		public async Task<int> Delete(int id)
		{
			int cantidad = await _MetodoPagoRepository.Delete(id);
			return cantidad;
		}

		public async Task<int> DeleteMultipleItems(List<MetodoPagoRequest> lista)
		{
			List<MetodoPago> MetodoPagos = _mapper.Map<List<MetodoPago>>(lista);
			int cantidad =await	_MetodoPagoRepository.DeleteMultipleItems(MetodoPagos);
			return cantidad;
		}

		public async Task<GenericFilterResponse<MetodoPagoResponse>> GetByFilter(GenericFilterRequest request)
		{

			GenericFilterResponse<MetodoPagoResponse> result = _mapper.Map<GenericFilterResponse<MetodoPagoResponse>>(_MetodoPagoRepository.GetByFilter(request));

			return result;
		}

		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<MetodoPagoResponse> Patch(int id, JsonPatchDocument<MetodoPagoRequest> patchDocument)
		{
			throw new NotImplementedException();
		}
		#endregion END CRUD METHODS
	}
}