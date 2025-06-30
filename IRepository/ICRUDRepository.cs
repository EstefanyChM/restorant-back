using BDRiccosModel;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    /// <summary>
    /// DISPONE DE TODOS LOS METODOS DEL CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICRUDRepository<T>: IDisposable
    {
        /***************************/
        // Nuevo método para manejar el estado de las entidades
    void Detach(T entity);  // Método para desacoplar una entidad

        /***********************/
        /// <summary>
        /// RETORNA TODA LA LISTA DE LA TABLA <typeparamref name="T"/>
        /// </summary>
        /// <returns> LISTA DE <typeparamref name="T"/> </returns>
        Task<List<T>> GetAll();


        IQueryable<T> GetAllQueryable();
       


        /// <summary>
        /// RETORNA UN REGISTRO DE LA TABLA FILTRADO POR EL PRIMARY KEY
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns> <typeparamref name="T"/> </returns>
        Task<T> GetById(object id);




        /// <summary>
        /// INSERTA UN REGISTRO A LA TABLA <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"> <typeparamref name="T"/> </param>
        /// <returns> <typeparamref name="T"/> </returns>
        Task<T> Create(T entity);


        /// <summary>
        /// ACTUALIZA UN REGISTRO A LA TABLA <typeparamref name="T"/>
        /// </summary>
        /// <param name="entity"> <typeparamref name="T"/> </param>
        /// <returns> <typeparamref name="T"/> </returns>
        Task<T> Update(T entity);


        /// <summary>
        /// ELIMINA UN REGISTRO DE LA TABLA FILTRADO POR EL PRIMARY KEY
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>int</returns>
       Task<int> Delete(object id);

        /// <summary>
        /// ELIMINADO Y RESTAURACIÓN LÓGICA DE UN REGISTRO DE LA TABLA FILTRADO POR EL PRIMARY KEY
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>int</returns>
        Task<int> LogicDelete(int id);



        Task<T> Existe(string propiedad, object valorPropiedad);

        Task<string> ExistsById(object id) ;

        /// <summary>
        /// Elimina varios registros de la tabla
        /// </summary>
        /// <param name="lista"> List<paramref name="lista"/> </param>
        /// <returns>int cantidad de registros eliminados</returns>
        Task<int> DeleteMultipleItems(List<T> lista);

        /// <summary>
        /// inserta varios registros en la tabla <typeparamref name="T"/>
        /// </summary>
        /// <param name="lista"> List<paramref name="lista"/> </param>
        /// <returns>List<paramref name="lista"/></returns>
        Task<List<T>> CreateMultiple(List<T> lista);

        /// <summary>
        /// actualizar varios registros en la tabla <typeparamref name="T"/>
        /// </summary>
        /// <param name="lista"> List<paramref name="lista"/> </param>
        /// <returns>List<paramref name="lista"/></returns>
        Task<List<T>> UpdateMultiple(List<T> lista);
        GenericFilterResponse<T> GetByFilter(GenericFilterRequest request);


    }
}
