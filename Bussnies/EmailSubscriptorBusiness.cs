using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using RequestResponseModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using IServices;
using Repository;

namespace Bussnies
{
    public class EmailSuscriptorBusiness : IEmailSuscriptorBusiness
    {
        /* INYECCIÓN DE DEPENDENCIAS */
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IMapper _mapper;
        private readonly IEmailSuscriptorRepository _emailSuscriptorRepository;
        private readonly IServicioEmailSendGrid _servicioEmailSendGrid;

        public EmailSuscriptorBusiness(IMapper mapper
            , IEmailSuscriptorRepository emailSuscriptorRepository
            , IServicioEmailSendGrid servicioEmailSendGrid)

        {
            _mapper = mapper;
            _emailSuscriptorRepository = emailSuscriptorRepository;
            _servicioEmailSendGrid = servicioEmailSendGrid;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS


        public async Task<List<EmailSuscriptorResponse>> GetAll()
        {
            List<EmailSuscriptor> suscriptores = await _emailSuscriptorRepository.GetAll();
            return _mapper.Map<List<EmailSuscriptorResponse>>(suscriptores);
        }

        public async Task<EmailSuscriptorResponse> GetById(int id)
        {
            EmailSuscriptor suscriptor = await _emailSuscriptorRepository.GetById(id);
            return _mapper.Map<EmailSuscriptorResponse>(suscriptor);
        }


        public async Task<EmailSuscriptorResponse> Create(EmailSuscriptorRequest entity)
        {
            entity.Estado = true;
            //entity.FechaSuscripcion = DateTime.Now;
            // Configura la zona horaria específica (ejemplo: Lima, Perú)
            TimeZoneInfo limaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            // Convierte la fecha actual UTC a la hora local de Lima
            entity.FechaSuscripcion = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, limaTimeZone);



            EmailSuscriptor suscriptor = _mapper.Map<EmailSuscriptor>(entity);
            suscriptor = await _emailSuscriptorRepository.Create(suscriptor);
            return _mapper.Map<EmailSuscriptorResponse>(suscriptor);
        }

        public async Task<EmailSuscriptorResponse> Update(EmailSuscriptorRequest entity)
        {
            EmailSuscriptor suscriptor = _mapper.Map<EmailSuscriptor>(entity);
            suscriptor = await _emailSuscriptorRepository.Update(suscriptor);
            return _mapper.Map<EmailSuscriptorResponse>(suscriptor);
        }

        public async Task<int> Delete(int id)
        {
            return await _emailSuscriptorRepository.Delete(id);
        }

        public async Task<EmailSuscriptorResponse> Patch(int id, JsonPatchDocument<EmailSuscriptorRequest> patchDocument)
        {
            EmailSuscriptor suscriptorDB = await _emailSuscriptorRepository.GetById(id);
            EmailSuscriptorRequest suscriptorRequest = _mapper.Map<EmailSuscriptorRequest>(suscriptorDB);

            patchDocument.ApplyTo(suscriptorRequest);
            _mapper.Map(suscriptorRequest, suscriptorDB);

            EmailSuscriptor updatedSuscriptor = await _emailSuscriptorRepository.Update(suscriptorDB);
            return _mapper.Map<EmailSuscriptorResponse>(updatedSuscriptor);
        }

        public async Task<int> LogicDelete(int id)
        {
            return await _emailSuscriptorRepository.LogicDelete(id);
        }

        public async Task<GenericFilterResponse<EmailSuscriptorResponse>> GetByFilter(GenericFilterRequest request)
        {
            GenericFilterResponse<EmailSuscriptorResponse> result = _mapper
                .Map<GenericFilterResponse<EmailSuscriptorResponse>>(_emailSuscriptorRepository
                .GetByFilter(request));

            return result;
        }



        public Task<int> DeleteMultipleItems(List<EmailSuscriptorRequest> lista)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmailSuscriptorResponse>> CreateMultiple(List<EmailSuscriptorRequest> lista)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmailSuscriptorResponse>> UpdateMultiple(List<EmailSuscriptorRequest> lista)
        {
            throw new NotImplementedException();
        }

        #endregion END CRUD METHODS
    }
}
