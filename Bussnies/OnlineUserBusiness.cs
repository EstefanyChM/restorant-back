using AutoMapper;
using BDRiccosModel;
using CommonModel;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.JsonPatch;
using RequestResponseModel;
using System;
using System.Collections.Generic;

namespace Bussnies
{
	public class OnlineUserBusiness : IOnlineUserBusiness
    {
        private readonly IOnlineUserRepository _onlineUserRepository;
		private readonly IClienteRepository _clienteRepository;
		private readonly IMapper _mapper;

        public OnlineUserBusiness(IMapper mapper, IOnlineUserRepository onlineUserRepository, IClienteRepository clienteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _onlineUserRepository = onlineUserRepository ?? throw new ArgumentNullException(nameof(onlineUserRepository));
			_clienteRepository = clienteRepository;
		}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<OnlineUserResponse>> GetAll()
        {
            List<OnlineUser> users = await _onlineUserRepository.GetAll();
            List<OnlineUserResponse> response = _mapper.Map<List<OnlineUserResponse>>(users);
            return response;
        }

        public async Task<OnlineUserResponse> GetById(int id)
        {
            OnlineUser user =await _onlineUserRepository.GetByIdWithCustomeDetails(id);
            OnlineUserResponse response = new OnlineUserResponse
            {
                Id = user.Id,
                Nombre = user.IdClienteNavigation.IdTablaPersonaNaturalNavigation.Nombre,
                Apellidos =user.IdClienteNavigation.IdTablaPersonaNaturalNavigation.Apellidos,
                FechaNacimiento =user.IdClienteNavigation.IdTablaPersonaNaturalNavigation.FechaNacimiento,
                Celular =user.IdClienteNavigation.IdTablaPersonaNaturalNavigation.Celular,
                NumeroDocumento = user.IdClienteNavigation.IdTablaPersonaNaturalNavigation.NumeroDocumento,
                IdPersonaTipoDocumento = user.IdClienteNavigation.IdTablaPersonaNaturalNavigation.IdPersonaTipoDocumento,
                Email = user.Email
            };
            //OnlineUserResponse response = _mapper.Map<OnlineUserResponse>(user);
            return response;
        }

        public async Task<OnlineUserResponse> Create(OnlineUserRequest entity)
        {
            OnlineUser user = _mapper.Map<OnlineUser>(entity);
            user =await _onlineUserRepository.Create(user);
            OnlineUserResponse response = _mapper.Map<OnlineUserResponse>(user);
            return response;
        }

        public async Task<List<OnlineUserResponse>> CreateMultiple(List<OnlineUserRequest> lista)
        {
            List<OnlineUser> users = _mapper.Map<List<OnlineUser>>(lista);
            users = await _onlineUserRepository.CreateMultiple(users);
            List<OnlineUserResponse> response = _mapper.Map<List<OnlineUserResponse>>(users);
            return response;
        }

        public async Task<OnlineUserResponse> Update(OnlineUserRequest entity)
        {
            OnlineUser user = _mapper.Map<OnlineUser>(entity);
            user = await _onlineUserRepository.Update(user);
            OnlineUserResponse response = _mapper.Map<OnlineUserResponse>(user);
            return response;
        }

        public async Task<List<OnlineUserResponse>> UpdateMultiple(List<OnlineUserRequest> lista)
        {
            List<OnlineUser> users = _mapper.Map<List<OnlineUser>>(lista);
            users = await _onlineUserRepository.UpdateMultiple(users);
            List<OnlineUserResponse> response = _mapper.Map<List<OnlineUserResponse>>(users);
            return response;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _onlineUserRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<OnlineUserRequest> lista)
        {
            List<OnlineUser> users = _mapper.Map<List<OnlineUser>>(lista);
            int cantidad = await _onlineUserRepository.DeleteMultipleItems(users);
            return cantidad;
        }

        public async Task<GenericFilterResponse<OnlineUserResponse>> GetByFilter(GenericFilterRequest request)
        {
            GenericFilterResponse<OnlineUserResponse> response = _mapper.Map<GenericFilterResponse<OnlineUserResponse>>(_onlineUserRepository.GetByFilter(request));
            return response;
        }

		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<OnlineUserResponse> Patch(int id, JsonPatchDocument<OnlineUserRequest> patchDocument)
		{
			throw new NotImplementedException();
		}
	}
}
