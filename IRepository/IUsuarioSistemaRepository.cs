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
    public interface IUsuarioSistemaRepository : ICRUDRepository<UsuariosSistema>
    {
        //Task<UsuariosSistema> GetById(int id);

        public Task<UsuariosSistema> ObtenerUsuarioSistema(string IdApplicationUser);
        Task<List<UsuariosSistema>> GetAllWithDates();


    }
}