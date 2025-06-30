using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using Repository;
using RequestResponseModel;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace Bussnies
{
    public class CarritoBussnies : ICarritoBussnies
    {
        /* INYECCIÓN DE DEPENDENCIAS */
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly ICarritoRepository _carritoRepository;
        private readonly IMapper _mapper;

        public CarritoBussnies(IMapper mapper)
        {
            _mapper = mapper;
            _carritoRepository = new CarritoRepository();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS
        public async Task<List<CarritoResponse>> GetAll()
        {
            // Obtén todos los carritos incluyendo los datos relacionados si es necesario
           // List<Carrito> carritos = _carritoRepository.GetAllQueryable().Include(c => c.Productos).ToList();
           // List<CarritoResponse> lstResponse = _mapper.Map<List<CarritoResponse>>(carritos);
            //return lstResponse;
            throw new NotImplementedException();

        }

        public async Task<CarritoResponse> GetById(int id)
        {
            Carrito carrito =await _carritoRepository.GetById(id);
            CarritoResponse result = _mapper.Map<CarritoResponse>(carrito);
            return result;
        }

        public async Task<CarritoResponse> Create(CarritoRequest entity)
        {
            Pedido pedidoEntity = new Pedido
            {
                Id = entity.Id, //Se crea el ID del un row de pedido
            };



            foreach (var detalle in entity.Detalles)
            {
                DetallePedido detalleEntity = new DetallePedido
                {

                };
                //_carritoRepository.Create(detalle);
            }


            Carrito carrito = _mapper.Map<Carrito>(entity);
            carrito =await _carritoRepository.Create(carrito);
            CarritoResponse result = _mapper.Map<CarritoResponse>(carrito);
            return result;
        }

        public async Task<List<CarritoResponse>> CreateMultiple(List<CarritoRequest> lista)
        {
            List<Carrito> carritos = _mapper.Map<List<Carrito>>(lista);
            carritos = await _carritoRepository.CreateMultiple(carritos);
            List<CarritoResponse> result = _mapper.Map<List<CarritoResponse>>(carritos);
            return result;
        }

        public async Task<CarritoResponse> Update(CarritoRequest entity)
        {
            Carrito carrito = _mapper.Map<Carrito>(entity);
            carrito =  await  _carritoRepository.Update(carrito);
            CarritoResponse result = _mapper.Map<CarritoResponse>(carrito);
            return result;
        }

        public async Task<List<CarritoResponse>> UpdateMultiple(List<CarritoRequest> lista)
        {
            List<Carrito> carritos = _mapper.Map<List<Carrito>>(lista);
            carritos = await _carritoRepository.UpdateMultiple(carritos);
            List<CarritoResponse> result = _mapper.Map<List<CarritoResponse>>(carritos);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad =  await _carritoRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<CarritoRequest> lista)
        {
            List<Carrito> carritos = _mapper.Map<List<Carrito>>(lista);
            int cantidad = await _carritoRepository.DeleteMultipleItems(carritos);
            return cantidad;
        }

        public async Task<GenericFilterResponse<CarritoResponse>> GetByFilter(GenericFilterRequest request)
        {
            GenericFilterResponse<CarritoResponse> result = _mapper
                .Map<GenericFilterResponse<CarritoResponse>>(_carritoRepository
                .GetByFilter(request));
            return result;
        }

        public async Task<CarritoResponse> Patch(int id, JsonPatchDocument<CarritoRequest> patchDocument)
        {
            var carritoDB = _carritoRepository.GetAllQueryable().FirstOrDefault(x => x.Id == id);
            var carritoPatched = _mapper.Map<CarritoRequest>(carritoDB);

            patchDocument.ApplyTo(carritoPatched);

            _mapper.Map(carritoPatched, carritoDB);
            _carritoRepository.Update(carritoDB);

            CarritoResponse result = _mapper.Map<CarritoResponse>(carritoDB);

            return result;
        }


		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}
		#endregion END CRUD METHODS
	}
}
