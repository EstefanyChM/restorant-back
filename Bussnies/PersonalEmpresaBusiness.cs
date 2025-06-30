using AutoMapper;
using BDRiccosModel;
using CommonModel;
using DTO;
using ExcepcionesPersonalizadas;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Repository;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Bussnies
{
    public class PersonalEmpresaBusiness : IPersonalEmpresaBusiness
    {
        private readonly IPersonaNaturalRepository _personaNaturalRepository;
        private readonly IPersonalEmpresaRepository _personalEmpresaRepository;
        private readonly IMapper _mapper;

        public PersonalEmpresaBusiness(IMapper mapper, IPersonaNaturalRepository personaNaturalRepository, IPersonalEmpresaRepository personalEmpresaRepository)
        {
            _mapper = mapper;
            _personaNaturalRepository = personaNaturalRepository;
            _personalEmpresaRepository = personalEmpresaRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonalEmpresaResponse>> GetAll()
        {
            List<PersonalEmpresa> personalEmpresas = await _personalEmpresaRepository.GetAllWithDates();

            return _mapper.Map<List<PersonalEmpresaResponse>>(personalEmpresas);
        }

        public async Task<PersonalEmpresaResponse> GetByIdUsuarioSistema(int idUsuario)
        {
            /*var p = await _personalEmpresaRepository.GetByIdUsuarioSistema(idUsuario);
            PersonalEmpresaResponse personalEmpresaResponse = new PersonalEmpresaResponse
            {

                Id = p.Id,
                IdPersonaNatural = p.IdPersonaNavigation.Id,
                Nombre = p.IdPersonaNavigation.Nombre,
                Apellidos = p.IdPersonaNavigation.Apellidos,
                FechaNacimiento = p.IdPersonaNavigation.FechaNacimiento,
                Celular = p.IdPersonaNavigation.Celular,
                Direccion = p.IdPersonaNavigation.Direccion,
                NumeroDocumento = p.IdPersonaNavigation.NumeroDocumento,

                IdPersonaTipoDocumento = p.IdPersonaNavigation.IdPersonaTipoDocumento,

                // Acceder a los UserRoles de cada UsuariosSistema
                UserRoles = p.UsuariosSistemas
                    .Select(us => us.IdApplicationUserNavigation.UserRoles) // Seleccionamos los UserRoles
                    .SelectMany(ur => ur)
                    .ToList(),

                Email = p.UsuariosSistemas
                    .Select(email => email.IdApplicationUserNavigation.Email).FirstOrDefault()

            };


            //return _mapper.Map<PersonalEmpresaResponse>(await _personalEmpresaRepository.GetByIdUsuarioSistema(idUsuario));
            return personalEmpresaResponse;*/

            throw new NotImplementedException();
        }

        public async Task<PersonalEmpresaResponse> GetById(int id)
        {
            PersonalEmpresa personnel = await _personalEmpresaRepository.GetById(id);
            PersonalEmpresaResponse response = _mapper.Map<PersonalEmpresaResponse>(personnel);
            return response;
        }

        public async Task<PersonalEmpresaResponse> Create(PersonalEmpresaCreateDTO entity)
        {
            // Buscar si ya existe una PersonaNatural con el mismo número de documento
            var personaNaturalDB = await _personaNaturalRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync(x => x.NumeroDocumento == entity.NumeroDocumento);

            if (personaNaturalDB != null)
            {
                // Buscar si la PersonaNatural ya está en PersonalEmpresa
                var personalEmpresaDB = await _personalEmpresaRepository
                    .GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.IdPersona == personaNaturalDB.Id);

                if (personalEmpresaDB != null)
                {
                    // PersonaNatural ya está registrada en PersonalEmpresa → Lanzar excepción
                    throw new DatoDuplicadoException(entity.NumeroDocumento);
                }
                // Si no existe en PersonalEmpresa, crearla
                return await CrearPersonalEmpresa(personaNaturalDB, entity.Email);
            }
            else
            {
                // Si no existe, primero crear la PersonaNatural
                var nuevaPersona = await _personaNaturalRepository.Create(_mapper.Map<PersonaNatural>(entity));

                // Luego, crear PersonalEmpresa
                return await CrearPersonalEmpresa(nuevaPersona, entity.Email);
            }
        }

        private async Task<PersonalEmpresaResponse> CrearPersonalEmpresa(PersonaNatural personaNatural, string email)
        {
            var personalEmpresa = new PersonalEmpresa
            {
                IdPersona = personaNatural.Id,
                Estado = true,
                Email = email
            };

            await _personalEmpresaRepository.Create(personalEmpresa);

            // Mapeo combinado de PersonaNatural y PersonalEmpresa
            var response = _mapper.Map<PersonalEmpresaResponse>(personaNatural);
            _mapper.Map(personalEmpresa, response); // Agregar datos de PersonalEmpresa

            return response;
        }


        public async Task<List<PersonalEmpresaResponse>> CreateMultiple(List<PersonalEmpresaRequest> lista)
        {
            List<PersonalEmpresa> personnel = _mapper.Map<List<PersonalEmpresa>>(lista);
            personnel = await _personalEmpresaRepository.CreateMultiple(personnel);
            List<PersonalEmpresaResponse> response = _mapper.Map<List<PersonalEmpresaResponse>>(personnel);
            return response;
        }

        public async Task<PersonalEmpresaResponse> Update(PersonalEmpresaRequest entity)
        {
            PersonalEmpresa personnel = _mapper.Map<PersonalEmpresa>(entity);
            personnel = await _personalEmpresaRepository.Update(personnel);
            PersonalEmpresaResponse response = _mapper.Map<PersonalEmpresaResponse>(personnel);
            return response;
        }

        public async Task<List<PersonalEmpresaResponse>> UpdateMultiple(List<PersonalEmpresaRequest> lista)
        {
            List<PersonalEmpresa> personnel = _mapper.Map<List<PersonalEmpresa>>(lista);
            personnel = await _personalEmpresaRepository.UpdateMultiple(personnel);
            List<PersonalEmpresaResponse> response = _mapper.Map<List<PersonalEmpresaResponse>>(personnel);
            return response;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _personalEmpresaRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<PersonalEmpresaRequest> lista)
        {
            List<PersonalEmpresa> personnel = _mapper.Map<List<PersonalEmpresa>>(lista);
            int cantidad = await _personalEmpresaRepository.DeleteMultipleItems(personnel);
            return cantidad;
        }

        public async Task<GenericFilterResponse<PersonalEmpresaResponse>> GetByFilter(GenericFilterRequest request)
        {
            GenericFilterResponse<PersonalEmpresaResponse> response = _mapper.Map<GenericFilterResponse<PersonalEmpresaResponse>>(_personalEmpresaRepository.GetByFilter(request));
            return response;
        }

        public Task<int> LogicDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalEmpresaResponse> Patch(int id, JsonPatchDocument<PersonalEmpresaRequest> patchDocument)
        {
            throw new NotImplementedException();
        }

        public Task<PersonalEmpresaResponse> Create(PersonalEmpresaRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
