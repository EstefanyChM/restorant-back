using BDRiccosModel;
using DTO;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class PersonaNaturalRepository : CRUDRepository<PersonaNatural>, IPersonaNaturalRepository
	{
		public GenericFilterResponse<PersonaNatural> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}


		/*public List<VPersonaUbigeo> ObtenerTodosConUbigeo()
        {
            List<VPersonaUbigeo> list = db.VPersonaUbigeos.ToList();
            return list;
        }*/
		public PersonaNatural GetByTipoNroDocumento(int tipoDocumento, string NroDocumento)
		{
			PersonaNatural personaNatural = new PersonaNatural();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			personaNatural = db.PersonaNaturals
				 .Where(x => x.IdPersonaTipoDocumento == tipoDocumento && x.NumeroDocumento == NroDocumento)
				 .Include(x => x.IdPersonaTipoDocumentoNavigation)
				 //.Include(x => x.IdPersonaTipoNavigation)
				 .FirstOrDefault()
				 ;

			return personaNatural;
		}

	}
}


