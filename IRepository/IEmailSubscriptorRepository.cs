using BDRiccosModel;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IEmailSuscriptorRepository : ICRUDRepository<EmailSuscriptor>
    {
        Task<List<string>> ObtenerSuscriptoresActivos();
    }
}
