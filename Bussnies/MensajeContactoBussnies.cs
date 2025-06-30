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
    public class MensajeContactoBussnies : IMensajeContactoBussnies
    {
        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IMapper _mapper;
		private readonly IMensajeContactoRepository _mensajeContactoRepository;
		private readonly IServicioEmailSendGrid _servicioEmailSendGrid;

		public MensajeContactoBussnies(IMapper mapper
            
            , IMensajeContactoRepository mensajeContactoRepository,
            IServicioEmailSendGrid servicioEmailSendGrid)
		{
			_mapper = mapper;
			this._mensajeContactoRepository = mensajeContactoRepository;
			_servicioEmailSendGrid = servicioEmailSendGrid;
		}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS
        public async Task<List<MensajeContactoResponse>> GetAll()
        {

            List<MensajeContacto> mensajeContacto = await GetAllConDetalles();

            List<MensajeContactoResponse> lstResponse = _mapper.Map<List<MensajeContactoResponse>>(mensajeContacto);

            return lstResponse;
        }

        public async Task<List<MensajeContacto>> GetAllConDetalles()
        {
            List<MensajeContacto> mensajeContacto = await _mensajeContactoRepository.GetAllConDetalles();
            return  mensajeContacto;
        }



        public async Task<MensajeContactoResponse> GetById(int id)
        {
            MensajeContacto mensajeContacto =await  _mensajeContactoRepository.GetById(id);
            MensajeContactoResponse result = _mapper.Map<MensajeContactoResponse>(mensajeContacto);
            return result;
        }

        public async Task<MensajeContactoResponse> Create(MensajeContactoRequest entity)
        {
            Remite remite = new Remite
            {
                Email = entity.RemiteEmail,
                Nombre = entity.RemiteNombre
            };
            Remite remitee =await  new CRUDRepository<Remite>().Create(remite); // PORQUE ME DA FLOJERA CREAR CLASES REPOSITORIO DE REMITE

            MensajeContacto mensajeContacto = new MensajeContacto
            {
               // IdRemiteNavigation.  = entity.RemiteEmail ,
                IdAsunto = entity.IdAsunto,
                IdRemite = remitee.Id,
                Mensaje = entity.Mensaje.Length > 10? entity.Mensaje.Substring(0,10) + " ...":entity.Mensaje,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                IdEstadoMensaje = 1,
                Estado = true
            };

            var mensajeContactoCreado =await _mensajeContactoRepository.Create(mensajeContacto);

            MensajeContactoResponse mandar = _mapper.Map<MensajeContactoResponse>(mensajeContactoCreado);
           /* await _servicioEmailSendGrid.EnviarMasivos(
                new Promocion { }, []
                
                );*/ /*!! PARA NO ENVIAR A SENDGRID lo comento*/

            return mandar;
        }


        
        public async Task<List<MensajeContactoResponse>> CreateMultiple(List<MensajeContactoRequest> lista)
        {
            List<MensajeContacto> mensajesContacto = _mapper.Map<List<MensajeContacto>>(lista);
            mensajesContacto = await _mensajeContactoRepository.CreateMultiple(mensajesContacto);
            List<MensajeContactoResponse> result = _mapper.Map<List<MensajeContactoResponse>>(mensajesContacto);
            return result;
        }

       public async Task<MensajeContactoResponse> Update(MensajeContactoRequest entity)
        {
            MensajeContacto mensajeContacto = _mapper.Map<MensajeContacto>(entity);
            mensajeContacto =await  _mensajeContactoRepository.Update(mensajeContacto);
            MensajeContactoResponse result = _mapper.Map<MensajeContactoResponse>(mensajeContacto);
            return result;
        }

        public async Task<List<MensajeContactoResponse>> UpdateMultiple(List<MensajeContactoRequest> lista)
        {
            List<MensajeContacto> mensajesContacto = _mapper.Map<List<MensajeContacto>>(lista);
            mensajesContacto = await _mensajeContactoRepository.UpdateMultiple(mensajesContacto);
            List<MensajeContactoResponse> result = _mapper.Map<List<MensajeContactoResponse>>(mensajesContacto);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _mensajeContactoRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<MensajeContactoRequest> lista)
        {
            List<MensajeContacto> mensajesContacto = _mapper.Map<List<MensajeContacto>>(lista);
            int cantidad = await _mensajeContactoRepository.DeleteMultipleItems(mensajesContacto);
            return cantidad;
        }

        public async Task<GenericFilterResponse<MensajeContactoResponse>> GetByFilter(GenericFilterRequest request)
        {
            var a = _mensajeContactoRepository.GetByFilter(request);

            GenericFilterResponse<MensajeContactoResponse> result = _mapper.Map<GenericFilterResponse<MensajeContactoResponse>>(a);
            return result;
        }

		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<MensajeContactoResponse> Patch(int id, JsonPatchDocument<MensajeContactoRequest> patchDocument)
		{
			throw new NotImplementedException();
		}


		#endregion END CRUD METHODS
	}
}
