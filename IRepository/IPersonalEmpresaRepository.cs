using Azure.Core;
using BDRiccosModel;
using CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
	public interface IPersonalEmpresaRepository : ICRUDRepository<PersonalEmpresa>
	{
		public Task<PersonalEmpresa> ObtenerPersonalEmpresa(string IdApplicationUser);
		public Task<bool> EsPersonalEmpresa(string Email);
		public Task<PersonalEmpresa> FindByEmailAsync(string Email);

		/***********BORRAR DE ARRIBA ********************/

		public PersonalEmpresa ObtenerPorIdPersona(int idPersona);

		public Task<List<PersonalEmpresa>> GetAllWithDates();

		public Task<PersonalEmpresa> GetByIdUsuarioSistema(int idUsuario);
	}
}