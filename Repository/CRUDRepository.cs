using Azure.Core;
using BDRiccosModel;
using CommonModel;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPersonalizadas;

namespace Repository
{

	public class CRUDRepository<TEntity> where TEntity : class//, ICRUDRepository<TEntity>
	{

		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
		protected readonly _dbRiccosContext db;
		protected readonly DbSet<TEntity> dbSet;

		public CRUDRepository(_dbRiccosContext _DbRiccosContext)
		{
			db = _DbRiccosContext;
			//db=db;
			dbSet = db.Set<TEntity>();
		}


		#endregion DECLARACIÓN DE VARIABLES

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


		// Nuevo método para obtener la entrada de una entidad
		public void Detach(TEntity entity)
		{
			db.Entry(entity).State = EntityState.Detached;
		}
		/****************************/
		#region MÉTODOS CRUD



		public async Task<List<TEntity>> GetAll()
		{
			IQueryable<TEntity> query = dbSet;
			return await query.ToListAsync();
		}

		public virtual async Task<TEntity> GetById(object id)
		{
			var a = await dbSet.FindAsync(id);
			return a;
		}

		public async Task<TEntity> Create(TEntity entity)
		{
			dbSet.Add(entity);
			await db.SaveChangesAsync();
			return entity;
		}




		public async Task<TEntity> Update(TEntity entity)
		{
			dbSet.Update(entity);
			await db.SaveChangesAsync();
			return entity;
		}


		//object puede ser entero, shor, string (tipo de dato primario)
		public async Task<int> Delete(object id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			dbSet.Remove(entityToDelete);
			return await db.SaveChangesAsync();
		}

		public async Task<int> LogicDelete(int id)//  con reflexión
		{
			TEntity entityToLogicDelete = await dbSet.FindAsync(id);

			PropertyInfo propertyInfo = typeof(TEntity).GetProperty("Estado");
			bool currentEstado = (bool)propertyInfo.GetValue(entityToLogicDelete);// (este casting es necesario porque GetValue devuelve un object).
			propertyInfo.SetValue(entityToLogicDelete, !currentEstado);
			return await db.SaveChangesAsync();
		}
		#endregion MÉTODOS CRUD


		#region wasawasa
		public async Task<List<TEntity>> CreateMultiple(List<TEntity> lista)
		{
			dbSet.AddRange(lista);
			db.SaveChangesAsync();
			return lista;
		}

		public async Task<int> DeleteMultipleItems(List<TEntity> lista)
		{
			throw new NotImplementedException();
		}

		public async Task<List<TEntity>> UpdateMultiple(List<TEntity> lista)
		{
			dbSet.UpdateRange(lista);
			db.SaveChangesAsync();
			return lista;
		}

		/***************************/
		public IQueryable<TEntity> GetAllQueryable()
		{
			return dbSet.AsQueryable();
		}
		/********************************/

		public async Task<TEntity> Existe(string propiedad, object valor)
		{
			// Verifica si la propiedad existe en la entidad  
			if (typeof(TEntity).GetProperty(propiedad) == null)
				throw new ArgumentException($"La propiedad '{propiedad}' no existe en '{typeof(TEntity).Name}'.");

			// Consulta optimizada con AnyAsync para evitar traer datos innecesarios  
			var entity = await dbSet.AsNoTracking()
									.FirstOrDefaultAsync(e => EF.Property<object>(e, propiedad).Equals(valor));

			return entity;
		}



		public async Task<string> ExistsById(object id)
		{
			return await dbSet.FindAsync(id) == null ? "ESE ID NO EXISTE, MI CIELA" : "El ID existe.";
		}
		#endregion wasawasa
	}
}
