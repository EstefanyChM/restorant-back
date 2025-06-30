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
    public class PersonaNaturalBussnies : IPersonaNaturalBussnies
    {
        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IPersonaNaturalRepository _PersonaNaturalRepository;

        private readonly IMapper _mapper;
        private readonly IApisPeruServices _apisPeruServices;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        public PersonaNaturalBussnies(IMapper mapper, IApisPeruServices apisPeruServices)
        {
            _mapper = mapper;
            _PersonaNaturalRepository = new PersonaNaturalRepository();
            _apisPeruServices = apisPeruServices;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS
        public async Task<List<PersonaNaturalResponse>> GetAll()
        {
            List<PersonaNatural> Personas = await _PersonaNaturalRepository.GetAll();
            List<PersonaNaturalResponse> lstResponse = _mapper.Map<List<PersonaNaturalResponse>>(Personas);
            return lstResponse;
        }

        public async Task<PersonaNaturalResponse> GetById(int id)
        {
            PersonaNatural Persona = await _PersonaNaturalRepository.GetById(id);
            PersonaNaturalResponse resul = _mapper.Map<PersonaNaturalResponse>(Persona);
            return resul;
        }

        public async Task<PersonaNaturalResponse> Create(PersonaNaturalRequest entity)
        {
            PersonaNatural Persona = _mapper.Map<PersonaNatural>(entity);
            Persona = await _PersonaNaturalRepository.Create(Persona);
            PersonaNaturalResponse result = _mapper.Map<PersonaNaturalResponse>(Persona);
            return result;
        }


        public async Task<List<PersonaNaturalResponse>> CreateMultiple(List<PersonaNaturalRequest> lista)
        {
            List<PersonaNatural> Personas = _mapper.Map<List<PersonaNatural>>(lista);
            Personas = await _PersonaNaturalRepository.CreateMultiple(Personas);
            List<PersonaNaturalResponse> result = _mapper.Map<List<PersonaNaturalResponse>>(Personas);
            return result;
        }
        string userRole = "";
        public async Task<PersonaNaturalResponse> Update(PersonaNaturalRequest entity)
        {
            PersonaNatural PersonaRequest = _mapper.Map<PersonaNatural>(entity);
            PersonaNaturalResponse PersonaOld = await GetById(entity.IdPersonaNatural);

            /* PersonaRequest.UsuarioCrea = PersonaOld.UsuarioCrea;
			 PersonaRequest.FechaActualiza = PersonaOld.FechaActualiza;*/

            PersonaRequest = await _PersonaNaturalRepository.Update(PersonaRequest);
            PersonaNaturalResponse result = _mapper.Map<PersonaNaturalResponse>(PersonaRequest);
            return result;
        }

        public async Task<List<PersonaNaturalResponse>> UpdateMultiple(List<PersonaNaturalRequest> lista)
        {
            List<PersonaNatural> Personas = _mapper.Map<List<PersonaNatural>>(lista);
            Personas = await _PersonaNaturalRepository.UpdateMultiple(Personas);
            List<PersonaNaturalResponse> result = _mapper.Map<List<PersonaNaturalResponse>>(Personas);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _PersonaNaturalRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<PersonaNaturalRequest> lista)
        {
            List<PersonaNatural> Personas = _mapper.Map<List<PersonaNatural>>(lista);
            int cantidad = await _PersonaNaturalRepository.DeleteMultipleItems(Personas);
            return cantidad;
        }

        /*public List<VPersonaUbigeo> ObtenerTodosConUbigeo()
        {
            List < VPersonaUbigeo > lst = _PersonaRepository.ObtenerTodosConUbigeo();
            return lst;
        }*/


        public async Task<GenericFilterResponse<PersonaNaturalResponse>> GetByFilter(GenericFilterRequest request)
        {
            throw new NotImplementedException();
        }

        #endregion END CRUD METHODS

        public PersonaNaturalResponse GetByTipoNroDocumento(int tipoDocumento, string NroDocumento)
        {
            // Se busca en la DB

            PersonaNatural personaConsulta = _PersonaNaturalRepository.GetByTipoNroDocumento(tipoDocumento, NroDocumento);

            //Si no está registrada en la DB
            if (personaConsulta == null || personaConsulta.Id == 0)
            {
                ApisPeruPersonaResponse pres = _apisPeruServices.PersonaPorDNI(NroDocumento); //LobuscaenApiReniec

                //Si lo encuentra
                if (pres.Success)
                {
                    personaConsulta = new PersonaNatural()
                    {
                        Nombre = pres.Nombres,
                        Apellidos = pres.ApellidoPaterno + ' ' + pres.ApellidoMaterno,
                        IdPersonaTipoDocumento = 1,
                        NumeroDocumento = NroDocumento,
                    };
                }
                else throw new Exception("DNI no encontrado"); // Lanzar una excepción
            }

            PersonaNaturalResponse response = _mapper.Map<PersonaNaturalResponse>(personaConsulta);

            return response;
        }

        public Task<int> LogicDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonaNaturalResponse> Patch(int id, JsonPatchDocument<PersonaNaturalRequest> patchDocument)
        {
            throw new NotImplementedException();
        }
    }
}