using BDRiccosModel;
using CommonModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Repository
{
	public class PersonalEmpresaRepository : CRUDRepository<PersonalEmpresa>, IPersonalEmpresaRepository
	{
		public PersonalEmpresaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public PersonalEmpresa ObtenerPorIdPersona(int idPersona)
		{
			PersonalEmpresa personalEmpresa = dbSet.FirstOrDefault(x => x.IdPersona == idPersona);

			return personalEmpresa;

		}


		/*************************************/
		public async Task<PersonalEmpresa> FindByEmailAsync(string Email)
		{
			PersonalEmpresa personalEmpresa = await dbSet.FirstOrDefaultAsync(peDB => peDB.Email == Email);
			return personalEmpresa;
		}


		public async Task<PersonalEmpresa> ObtenerPersonalEmpresa(string IdApplicationUser)
		{
			/*PersonalEmpresa personalEmpresa = await dbSet
                .FirstOrDefaultAsync(ou => ou.IdApplicationUser == IdApplicationUser);
			
			return personalEmpresa;*/
			throw new NotImplementedException();

		}

		public async Task<bool> EsPersonalEmpresa(string Email)
		{
			return await dbSet.AnyAsync(u => u.Email == Email);
		}


		public GenericFilterResponse<PersonalEmpresa> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}


		public async Task<List<PersonalEmpresa>> GetAllWithDates()
		{
			List<PersonalEmpresa> personalEmpresas = await dbSet
			.Include(x => x.IdPersonaNavigation).ToListAsync();

			return personalEmpresas;
		}

		public async Task<PersonalEmpresa> GetByIdUsuarioSistema(int idUsuario)
		{
			UsuariosSistema usuariosSistema = db.UsuariosSistemas.FirstOrDefault(x => x.Id == idUsuario);

			/*PersonalEmpresa personalEmpresa = dbSet.Include(x => x.IdPersonaNavigation).FirstOrDefault(x => x.Id == usuariosSistema.IdPersonalEmpresa);*/

			PersonalEmpresa personalEmpresa = dbSet
			.Include(x => x.IdPersonaNavigation)
			.Include(x => x.UsuariosSistemas)
				.ThenInclude(x => x.IdApplicationUserNavigation.UserRoles)
				.FirstOrDefault(x => x.Id == usuariosSistema.IdPersonalEmpresa)
				   ;



			return personalEmpresa;
		}
	}



}
