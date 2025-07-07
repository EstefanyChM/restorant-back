using AutoMapper;
using BDRiccosModel;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using ExcepcionesPersonalizadas;
using IBussnies;
using IRepository;
using IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Repository;
using RequestResponseModel;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussnies
{
	public class PersonaJuridicaBussnies : IPersonaJuridicaBussnies
	{
		/*INYECCIÓN DE DEPENDECIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		private readonly IPersonaJuridicaRepository _PersonaJuridicaRepository;

		private readonly IMapper _mapper;
		private readonly IApisPeruServices _apisPeruServices;
		private readonly IPersonaJuridicaRepository personaJuridicaRepository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mapper"></param>
		public PersonaJuridicaBussnies(IMapper mapper, IApisPeruServices apisPeruServices, IPersonaJuridicaRepository personaJuridicaRepository)
		{
			_mapper = mapper;
			_apisPeruServices = apisPeruServices;
			_PersonaJuridicaRepository = personaJuridicaRepository;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<PersonaJuridicaResponse>> GetAll()
		{
			List<PersonaJuridica> Personas = await _PersonaJuridicaRepository.GetAll();
			List<PersonaJuridicaResponse> lstResponse = _mapper.Map<List<PersonaJuridicaResponse>>(Personas);
			return lstResponse;
		}

		public async Task<PersonaJuridicaResponse> GetById(int id)
		{
			PersonaJuridica Persona = await _PersonaJuridicaRepository.GetById(id);
			PersonaJuridicaResponse resul = _mapper.Map<PersonaJuridicaResponse>(Persona);
			return resul;
		}

		public async Task<PersonaJuridicaResponse> Create(PersonaJuridicaRequest entity)
		{
			PersonaJuridica Persona = _mapper.Map<PersonaJuridica>(entity);
			Persona = await _PersonaJuridicaRepository.Create(Persona);
			PersonaJuridicaResponse result = _mapper.Map<PersonaJuridicaResponse>(Persona);
			return result;
		}


		public async Task<List<PersonaJuridicaResponse>> CreateMultiple(List<PersonaJuridicaRequest> lista)
		{
			List<PersonaJuridica> Personas = _mapper.Map<List<PersonaJuridica>>(lista);
			Personas = await _PersonaJuridicaRepository.CreateMultiple(Personas);
			List<PersonaJuridicaResponse> result = _mapper.Map<List<PersonaJuridicaResponse>>(Personas);
			return result;
		}
		string userRole = "";
		public async Task<PersonaJuridicaResponse> Update(PersonaJuridicaRequest entity)
		{
			PersonaJuridica PersonaRequest = _mapper.Map<PersonaJuridica>(entity);
			PersonaJuridicaResponse PersonaOld = await GetById(entity.IdPersonaJuridica);

			PersonaRequest = await _PersonaJuridicaRepository.Update(PersonaRequest);
			PersonaJuridicaResponse result = _mapper.Map<PersonaJuridicaResponse>(PersonaRequest);
			return result;
		}

		public async Task<List<PersonaJuridicaResponse>> UpdateMultiple(List<PersonaJuridicaRequest> lista)
		{
			List<PersonaJuridica> Personas = _mapper.Map<List<PersonaJuridica>>(lista);
			Personas = await _PersonaJuridicaRepository.UpdateMultiple(Personas);
			List<PersonaJuridicaResponse> result = _mapper.Map<List<PersonaJuridicaResponse>>(Personas);
			return result;
		}

		public async Task<int> Delete(int id)
		{
			int cantidad = await _PersonaJuridicaRepository.Delete(id);
			return cantidad;
		}

		public async Task<int> DeleteMultipleItems(List<PersonaJuridicaRequest> lista)
		{
			List<PersonaJuridica> Personas = _mapper.Map<List<PersonaJuridica>>(lista);
			int cantidad = await _PersonaJuridicaRepository.DeleteMultipleItems(Personas);
			return cantidad;
		}

		/*public List<VPersonaUbigeo> ObtenerTodosConUbigeo()
        {
            List < VPersonaUbigeo > lst = _PersonaRepository.ObtenerTodosConUbigeo();
            return lst;
        }*/


		public async Task<GenericFilterResponse<PersonaJuridicaResponse>> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		#endregion END CRUD METHODS

		public PersonaJuridicaResponse GetByTipoNroDocumento(int tipoDocumento, string NroDocumento)
		{
			// Se busca en la DB
			PersonaJuridica personaConsulta = _PersonaJuridicaRepository.GetByTipoNroDocumento(tipoDocumento, NroDocumento);

			//Si no está registrada en la DB
			if (personaConsulta == null || personaConsulta.Id == 0)
			{
				ApisPeruEmpresaResponse pres = _apisPeruServices.EmpresaPorRUC(NroDocumento);

				if (pres.ruc != null)
				{
					personaConsulta = new PersonaJuridica()
					{
						Ruc = pres.ruc,
						RazonSocial = pres.razonSocial,
						IdPersonaTipoDocumento = 2,
					};
				}
				else throw new Exception("RUC no encontrado"); // Lanzar una excepción
			}
			PersonaJuridicaResponse response = _mapper.Map<PersonaJuridicaResponse>(personaConsulta);
			return response;
		}

		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<PersonaJuridicaResponse> Patch(int id, JsonPatchDocument<PersonaJuridicaRequest> patchDocument)
		{
			throw new NotImplementedException();
		}
	}
}
