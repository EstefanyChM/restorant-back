using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using Repository;
using RequestResponseModel;
using DTO;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http.HttpResults;
using Firebase.Auth;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using IServices;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Bussnies
{
	public class EmpresaBusiness : IEmpresaBusiness
	{
		/*INYECCIÓN DE DEPENDENCIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		private readonly IMapper _mapper;
		private readonly IFilesServices _filesServices;
		private readonly IEmpresaRepository _empresaRepository;

		public EmpresaBusiness(IMapper mapper, IFilesServices filesServices, IEmpresaRepository empresaRepository)
		{
			_mapper = mapper;
			_filesServices = filesServices;
			_empresaRepository = empresaRepository;
			;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		string carpeta = "Empresa";

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<EmpresaResponse>> GetAll()
		{
			List<Empresa> empresas = await _empresaRepository.GetAllWithSchedules();

			List<EmpresaResponse> lstResponse = _mapper.Map<List<EmpresaResponse>>(empresas);
			return lstResponse;
		}

		public async Task<EmpresaResponse> GetById(int id)
		{
			Empresa empresa = await _empresaRepository.GetById(id);

			EmpresaResponse result = _mapper.Map<EmpresaResponse>(empresa);
			return result;
		}

		public async Task<EmpresaResponse> Create(EmpresaRequest entity)
		{
			entity.Urllogo = await _filesServices.SubirArchivo(
					entity.Foto.OpenReadStream(),
					entity.Foto.FileName,
					carpeta
					);

			Empresa empresa = _mapper.Map<Empresa>(entity);
			empresa = await _empresaRepository.Create(empresa);
			EmpresaResponse result = _mapper.Map<EmpresaResponse>(empresa);
			return result;
		}

		public async Task<EmpresaResponse> Update(EmpresaRequest entity)
		{
			if (entity.Foto != null)
				entity.Urllogo = await _filesServices.SubirArchivo(
					entity.Foto.OpenReadStream(),
					entity.Foto.FileName,
					carpeta
					);

			Empresa empresa = _mapper.Map<Empresa>(entity);
			empresa = await _empresaRepository.Update(empresa);
			EmpresaResponse result = _mapper.Map<EmpresaResponse>(empresa);
			return result;
		}

		public async Task<List<EmpresaResponse>> CreateMultiple(List<EmpresaRequest> lista)
		{
			List<Empresa> empresas = _mapper.Map<List<Empresa>>(lista);
			empresas = await _empresaRepository.CreateMultiple(empresas);
			List<EmpresaResponse> result = _mapper.Map<List<EmpresaResponse>>(empresas);
			return result;
		}

		public async Task<List<EmpresaResponse>> UpdateMultiple(List<EmpresaRequest> lista)
		{
			List<Empresa> empresas = _mapper.Map<List<Empresa>>(lista);
			empresas = await _empresaRepository.UpdateMultiple(empresas);
			List<EmpresaResponse> result = _mapper.Map<List<EmpresaResponse>>(empresas);
			return result;
		}

		public async Task<int> Delete(int id)
		{
			int cantidad = await _empresaRepository.Delete(id);
			return cantidad;
		}

		public async Task<int> DeleteMultipleItems(List<EmpresaRequest> lista)
		{
			List<Empresa> empresas = _mapper.Map<List<Empresa>>(lista);
			int cantidad = await _empresaRepository.DeleteMultipleItems(empresas);
			return cantidad;
		}

		public async Task<EmpresaResponse> Patch(int id, JsonPatchDocument<EmpresaRequest> patchDocument)
		{
			Empresa empresaDB = await _empresaRepository.GetById(id);

			EmpresaRequest empDBRequest = _mapper.Map<EmpresaRequest>(empresaDB);

			patchDocument.ApplyTo(empDBRequest);
			_mapper.Map(empDBRequest, empresaDB);

			Empresa emp = await _empresaRepository.Update(empresaDB);
			EmpresaResponse result = _mapper.Map<EmpresaResponse>(emp);
			return result;
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _empresaRepository.LogicDelete(id);
			return result;
		}

		public async Task<GenericFilterResponse<EmpresaResponse>> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		#endregion END CRUD METHODS
	}
}
