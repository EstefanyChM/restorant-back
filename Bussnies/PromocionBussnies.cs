using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using Repository;
using RequestResponseModel;
using DTO;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http.HttpResults;
using Firebase.Auth;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using IServices;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Bussnies
{
	public class PromocionBussnies : IPromocionBussnies
	{
		/*INYECCIÓN DE DEPENDENCIAS*/
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		private readonly IPromocionRepository _promocionRepository;
		private readonly IMapper _mapper;
		private readonly IFilesServices _filesServices;
		private readonly IServicioEmailSendGrid _servicioEmailSendGrid;
		private readonly IProductoRepository _productoRepository;

		public PromocionBussnies(IMapper mapper, IFilesServices filesServices, IServicioEmailSendGrid servicioEmailSendGrid, IProductoRepository productoRepository)
		{
			_mapper = mapper;
			_filesServices = filesServices;
			_servicioEmailSendGrid = servicioEmailSendGrid;
			_productoRepository = productoRepository;
			_promocionRepository = new PromocionRepository();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		string carpeta = "Promocion";

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<PromocionResponse>> GetAll()
		{
			List<Promocion> promociones = await _promocionRepository.GetAll();

			List<PromocionResponse> lstResponse = _mapper.Map<List<PromocionResponse>>(promociones);
			return lstResponse;
		}

		public async Task EnviarCorreosMasivos(int id)
		{
			List<EmailSuscriptor> listaEmailSuscriptor = await new CRUDRepository<EmailSuscriptor>().GetAll();

			List<string> correosActivos = listaEmailSuscriptor
			.Where(s => s.Estado == true)
			.Select(s => s.Email)
			.ToList();


			Promocion promocion = await _promocionRepository.GetById(id);
			await _servicioEmailSendGrid.EnviarMasivos(promocion, correosActivos);
		}


		public async Task<PromocionResponse> GetById(int id)
		{
			Promocion promocion = await _promocionRepository.GetById(id);

			PromocionResponse result = _mapper.Map<PromocionResponse>(promocion);
			return result;
		}

		public async Task<PromocionResponse> Create(PromocionRequest entity)
		{
			decimal totalConDescuento = 0;
			// Mapear la entidad
			Promocion promocion = _mapper.Map<Promocion>(entity);

			// Crear una lista para almacenar los valores de stock
			List<int> stockValues = new List<int>();

			// Obtener el stock de cada producto a partir de los IdProducto
			foreach (var detalle in entity.DetallesPromocions)
			{
				Producto producto = await _productoRepository.GetById(detalle.IdProducto);

				// Asegurarse de que el producto exista antes de acceder a su stock
				if (producto != null)
				{
					stockValues.Add(producto.Stock);
				}

				totalConDescuento += producto.Precio * detalle.Cantidad * (1 - (detalle.PorcentajeDescuentoPorUnidad ?? 0));

			}

			// Si hay productos, obtener el valor mínimo del stock
			int minStock = stockValues.Any() ? stockValues.Min() : 0;

			// Aquí puedes usar el valor mínimo del stock para lo que necesites
			promocion.Stock = minStock;
			promocion.Total = Math.Ceiling(totalConDescuento);

			// Guardar la promoción en la base de datos
			promocion = await _promocionRepository.Create(promocion);

			// Mapear el resultado de la promoción
			PromocionResponse result = _mapper.Map<PromocionResponse>(promocion);

			return result;
		}



		public async Task<PromocionResponse> SubirImagen(int promocionId, IFormFile foto)
		{
			// if (await _promocionRepository.Existe("Nombre", entity.Nombre) != null) throw new DatoDuplicadoException(entity.Nombre);
			Promocion promocion = await _promocionRepository.GetById(promocionId);

			promocion.UrlImagen = await _filesServices.SubirArchivo(
					foto.OpenReadStream(),
					foto.FileName,
					carpeta
					);
			promocion = await _promocionRepository.Update(promocion);
			PromocionResponse result = _mapper.Map<PromocionResponse>(promocion);
			return result;
		}

		public async Task<PromocionResponse> Update(PromocionRequest entity)
		{
			//Promocion buscar = await _promocionRepository.Existe("Nombre", entity.Nombre);

			// Primero verificar si 'buscar' es null
			/*if (buscar != null && buscar.Id != entity.Id)
            {
                throw new DatoDuplicadoException(entity.Nombre);
            }*/

			/*if (entity.Foto != null) //Si lo que se cambió es la foto
                entity.UrlImagen = await _filesServices.SubirArchivo(
                    entity.Foto.OpenReadStream(),
                    entity.Foto.FileName,
                    carpeta
                    );*/

			Promocion promocion = _mapper.Map<Promocion>(entity);
			promocion = await _promocionRepository.Update(promocion);
			PromocionResponse result = _mapper.Map<PromocionResponse>(promocion);
			return result;
		}



		public async Task<List<PromocionResponse>> CreateMultiple(List<PromocionRequest> lista)
		{
			List<Promocion> promociones = _mapper.Map<List<Promocion>>(lista);
			promociones = await _promocionRepository.CreateMultiple(promociones);
			List<PromocionResponse> result = _mapper.Map<List<PromocionResponse>>(promociones);
			return result;
		}

		public async Task<List<PromocionResponse>> UpdateMultiple(List<PromocionRequest> lista)
		{
			List<Promocion> promociones = _mapper.Map<List<Promocion>>(lista);
			promociones = await _promocionRepository.UpdateMultiple(promociones);
			List<PromocionResponse> result = _mapper.Map<List<PromocionResponse>>(promociones);
			return result;
		}

		public async Task<int> Delete(int id)
		{
			int cantidad = await _promocionRepository.Delete(id);
			return cantidad;
		}

		public async Task<int> DeleteMultipleItems(List<PromocionRequest> lista)
		{
			List<Promocion> promociones = _mapper.Map<List<Promocion>>(lista);
			int cantidad = await _promocionRepository.DeleteMultipleItems(promociones);
			return cantidad;
		}

		public async Task<PromocionResponse> Patch(int id, JsonPatchDocument<PromocionRequest> patchDocument)
		{
			Promocion promocionDB = await _promocionRepository.GetById(id);

			PromocionRequest promoDBRequest = _mapper.Map<PromocionRequest>(promocionDB);

			patchDocument.ApplyTo(promoDBRequest); //Soy una copia de la entidad, pero con cambios
			_mapper.Map(promoDBRequest, promocionDB); //mapear los cambios rastreada por Entity Framework.

			Promocion promo = await _promocionRepository.Update(promocionDB);
			PromocionResponse result = _mapper.Map<PromocionResponse>(promo);
			return result;
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _promocionRepository.LogicDelete(id);
			return result;
		}

		public async Task<GenericFilterResponse<PromocionResponse>> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}
		#endregion END CRUD METHODS
	}
}
