using AutoMapper;
using BDRiccosModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using EllipticCurve.Utils;
using IBussnies;
using IRepository;
using IServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using RequestResponseModel;
using System;
using System.Collections.Generic;

namespace Bussnies
{
    public class MensajeBusiness : IMensajeBusiness
    {
        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IMapper _mapper;
		private readonly IEmailService _emailService;

		public MensajeBusiness(IMapper mapper
            , IEmailService emailService)
		{
			_mapper = mapper;
			_emailService = emailService;
		}

		public Task<MensajeContactoResponse> Create(MensajeContactoRequest entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MensajeContactoResponse>> CreateMultiple(List<MensajeContactoRequest> lista)
		{
			throw new NotImplementedException();
		}

		public Task<int> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<int> DeleteMultipleItems(List<MensajeContactoRequest> lista)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS


		public async Task EnviarMensajeAsync(MensajeContactoRequest request)
        {
            // Lógica de negocio (si hay) y delega el envío del correo
            await _emailService.SendEmailAsync(request);
        }


		/**************************/
		public Task<List<MensajeContactoResponse>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<GenericFilterResponse<MensajeContactoResponse>> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<MensajeContactoResponse> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<MensajeContactoResponse> Patch(int id, JsonPatchDocument<MensajeContactoRequest> patchDocument)
		{
			throw new NotImplementedException();
		}

		public Task<MensajeContactoResponse> Update(MensajeContactoRequest entity)
		{
			throw new NotImplementedException();
		}

		public Task<List<MensajeContactoResponse>> UpdateMultiple(List<MensajeContactoRequest> lista)
		{
			throw new NotImplementedException();
		}



		#endregion END CRUD METHODS
	}
}
