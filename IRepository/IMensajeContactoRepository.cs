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
	public interface IMensajeContactoRepository : ICRUDRepository<MensajeContacto>
	{
		Task<List<MensajeContacto>> GetAllConDetalles();


		/*****************************************/
		public Remite ObtenerRemitentePorId(int idRemitente);
		public Task<Remite> CrearRemitente(Remite remite);

		/***************************/

	}
}
