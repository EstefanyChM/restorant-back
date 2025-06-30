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
    public class UsuarioSistemaBusiness : IUsuarioSistemaBusiness
    {
        private readonly IUsuarioSistemaRepository _usuarioSistemaRepository;
        private readonly IMapper _mapper;

        public UsuarioSistemaBusiness(IMapper mapper,
            IUsuarioSistemaRepository usuarioSistemaRepository)
        {
            _mapper = mapper;
            _usuarioSistemaRepository = usuarioSistemaRepository;
        }

        public Task<UsuariosSistemaResponse> Create(UsuariosSistemaRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<UsuariosSistemaResponse>> CreateMultiple(List<UsuariosSistemaRequest> lista)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMultipleItems(List<UsuariosSistemaRequest> lista)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<UsuariosSistemaResponse>> GetAll()
        {
            List<UsuariosSistema> usuariosSistemaConSusDatosCompletos = await _usuarioSistemaRepository.GetAllWithDates();

            var a = _mapper.Map<List<UsuariosSistemaResponse>>(usuariosSistemaConSusDatosCompletos);

            return a;
        }

        public Task<GenericFilterResponse<UsuariosSistemaResponse>> GetByFilter(GenericFilterRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuariosSistemaResponse> GetById(int id)
        {
            UsuariosSistema usuariosSistema = await _usuarioSistemaRepository.GetById(id);
            return _mapper.Map<UsuariosSistemaResponse>(usuariosSistema);
        }



        public Task<int> LogicDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UsuariosSistemaResponse> Patch(int id, JsonPatchDocument<UsuariosSistemaRequest> patchDocument)
        {
            throw new NotImplementedException();
        }

        public Task<UsuariosSistemaResponse> Update(UsuariosSistemaRequest entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<UsuariosSistemaResponse>> UpdateMultiple(List<UsuariosSistemaRequest> lista)
        {
            throw new NotImplementedException();
        }
    }
}
