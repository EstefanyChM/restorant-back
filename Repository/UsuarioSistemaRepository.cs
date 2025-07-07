using BDRiccosModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;

namespace Repository
{
	public class UsuarioSistemaRepository : CRUDRepository<UsuariosSistema>, IUsuarioSistemaRepository
	{
		public UsuarioSistemaRepository(_dbRiccosContext _DbRiccosContext) : base(_DbRiccosContext)
		{
		}

		public override async Task<UsuariosSistema> GetById(object id)
		{
			int userId = Convert.ToInt32(id);
			UsuariosSistema usuarioSistema = await dbSet
				.Include(y => y.IdApplicationUserNavigation)
				.ThenInclude(y => y.UserRoles)
				.ThenInclude(y => y.Role)
				.Include(x => x.IdPersonalEmpresaNavigation)
				.ThenInclude(x => x.IdPersonaNavigation)
				.FirstOrDefaultAsync(x => x.Id == userId)
				;
			return usuarioSistema;
		}
		public async Task<UsuariosSistema> ObtenerUsuarioSistema(string IdApplicationUser)
		{
			UsuariosSistema us = await dbSet
				.FirstOrDefaultAsync(ou => ou.IdApplicationUser == IdApplicationUser);

			return us;
		}





		public GenericFilterResponse<UsuariosSistema> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}

		public async Task<List<UsuariosSistema>> GetAllWithDates()
		{
			List<UsuariosSistema> usuariosSistema = await dbSet
				.Include(y => y.IdApplicationUserNavigation)
				.ThenInclude(y => y.UserRoles)
				.ThenInclude(y => y.Role)
				.Include(x => x.IdPersonalEmpresaNavigation)
				.ThenInclude(x => x.IdPersonaNavigation)
				.ToListAsync()
				;
			return usuariosSistema;
		}

	}
}
