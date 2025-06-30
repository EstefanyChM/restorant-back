
using Microsoft.AspNetCore.JsonPatch;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBussnies
{
	/// <summary>
	/// DECLARA LOS METODOS DEL CRUD 
	/// </summary>
	/// <typeparam name="T">REQUEST</typeparam>
	/// <typeparam name="Y">RESPONSE</typeparam>
	public interface ICRUDBussnies<T, Y> : IDisposable where T : class //JsonPatchDocument<T> requiere que su parámetro de tipo sea un tipo de referencia.
	{
		Task<List<Y>> GetAll();
		Task<Y> GetById(int id);
		Task<Y> Create(T entity);
		Task<Y> Update(T entity);
		Task<int> Delete(int id);
		Task<int> LogicDelete(int id);
		Task<Y> Patch(int id, JsonPatchDocument<T> patchDocument);

		/*************************************/

		Task<int> DeleteMultipleItems(List<T> lista);
		Task<List<Y>> CreateMultiple(List<T> lista);
		Task<List<Y>> UpdateMultiple(List<T> lista);

		Task<GenericFilterResponse<Y>> GetByFilter(GenericFilterRequest request);

	}
}
