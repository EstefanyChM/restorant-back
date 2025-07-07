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
	public class PersonaJuridicaRepository : CRUDRepository<PersonaJuridica>, IPersonaJuridicaRepository
	{
		public PersonaJuridicaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public GenericFilterResponse<PersonaJuridica> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}
		public PersonaJuridica GetByTipoNroDocumento(int tipoDocumento, string NroDocumento)
		{
			PersonaJuridica persona = db.PersonaJuridicas
				.Where(x => x.IdPersonaTipoDocumento == 2 && x.Ruc == NroDocumento)
				.FirstOrDefault()
				;

			return persona;
		}

	}
}


