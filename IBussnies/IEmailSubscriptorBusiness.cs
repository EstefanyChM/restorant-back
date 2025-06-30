using BDRiccosModel;
using RequestResponseModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace IBussnies
{
    public interface IEmailSuscriptorBusiness : ICRUDBussnies<EmailSuscriptorRequest, EmailSuscriptorResponse>
    {
    }
}
