using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using Repository;
using RequestResponseModel;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using IServices;

namespace Bussnies
{
	public class ProductoBussnies : IProductoBussnies
	{
		/* INYECCIÓN DE DEPENDENCIAS */
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
		private readonly IProductoRepository _productoRepository;
		private readonly IMapper _mapper;
		private readonly IFilesServices _filesServices;
		private readonly ICategoriaRepository _categoriaRepository;

		public ProductoBussnies(IMapper mapper, IFilesServices filesServices, ICategoriaRepository categoriaRepository, IProductoRepository productoRepository
			)
		{
			_mapper = mapper;
			this._filesServices = filesServices;
			_categoriaRepository = categoriaRepository;
			_productoRepository = productoRepository;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		string carpeta = "Producto";

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<ProductoResponse>> GetAll()
		{
			List<Producto> productosConCateg = await GetAllWithDetails();
			//List<Producto> productosConCateg =await _productoRepository.GetAll();

			List<ProductoResponse> lstResponse = _mapper.Map<List<ProductoResponse>>(productosConCateg);

			return lstResponse;
		}

		public async Task<List<Producto>> GetAllWithDetails()
		{

			List<Producto> productos = await _productoRepository.GetAllWithDetails();
			return productos;
		}


		public async Task<ProductoResponse> GetById(int id)
		{
			Producto producto = await _productoRepository.GetById(id);
			ProductoResponse result = _mapper.Map<ProductoResponse>(producto);
			return result;
		}

		public async Task<ProductoResponse> Create(ProductoRequest entity)
		{
			if (await _productoRepository.Existe("Nombre", entity.Nombre) != null) throw new DatoDuplicadoException(entity.Nombre);


			entity.UrlImagen = await _filesServices.SubirArchivo(
					entity.Foto.OpenReadStream(),
					entity.Foto.FileName,
					carpeta
					);

			Producto producto = _mapper.Map<Producto>(entity);
			producto.Estado = true;
			producto = await _productoRepository.Create(producto);
			ProductoResponse result = _mapper.Map<ProductoResponse>(producto);
			return result;
		}

		public async Task<List<ProductoResponse>> CreateMultiple(List<ProductoRequest> lista)
		{
			List<Producto> productos = _mapper.Map<List<Producto>>(lista);
			productos = await _productoRepository.CreateMultiple(productos);
			List<ProductoResponse> result = _mapper.Map<List<ProductoResponse>>(productos);
			return result;
		}

		public async Task<ProductoResponse> Update(ProductoRequest entity)
		{
			Producto buscar = await _productoRepository.Existe("Nombre", entity.Nombre);

			if (buscar != null && buscar.Id != entity.Id)
			{
				throw new DatoDuplicadoException(entity.Nombre);
			}

			if (entity.Foto != null)//Si lo que se cambió es la foto
				entity.UrlImagen = await _filesServices.SubirArchivo(
					entity.Foto.OpenReadStream(),
					entity.Foto.FileName,
					carpeta
					);

			Categoria categoria = await _categoriaRepository.GetById(entity.IdCategoria);

			if (!categoria.Disponibilidad)
			{
				throw new CategoriaNoDisponibleException(categoria.Nombre);
			}

			Producto producto = _mapper.Map<Producto>(entity);
			producto = await _productoRepository.Update(producto);
			ProductoResponse result = _mapper.Map<ProductoResponse>(producto);

			return result;
		}


		public async Task<List<ProductoResponse>> UpdateMultiple(List<ProductoRequest> lista)
		{
			List<Producto> productos = _mapper.Map<List<Producto>>(lista);
			productos = await _productoRepository.UpdateMultiple(productos);
			List<ProductoResponse> result = _mapper.Map<List<ProductoResponse>>(productos);
			return result;
		}

		public async Task<List<ProductoResponse>> UpdateMultipleByCategory(int idCategory, bool newDisponibility)
		{
			List<Producto> productosFiltradosPorCategoria = _productoRepository.GetAllQueryable().Where(x => x.IdCategoria == idCategory).ToList();

			List<Producto> productos = _mapper.Map<List<Producto>>(productosFiltradosPorCategoria);

			//Cambiar Disponibilidad de todos los productos
			productos.ForEach(producto => producto.Disponibilidad = newDisponibility);

			productos = await _productoRepository.UpdateMultiple(productos);
			List<ProductoResponse> result = _mapper.Map<List<ProductoResponse>>(productos);
			return result;
		}

		public async Task<int> Delete(int id)
		{
			int cantidad = await _productoRepository.Delete(id);
			return cantidad;
		}

		public async Task<int> DeleteMultipleItems(List<ProductoRequest> lista)
		{
			List<Producto> productos = _mapper.Map<List<Producto>>(lista);
			int cantidad = await _productoRepository.DeleteMultipleItems(productos);
			return cantidad;
		}

		public async Task<GenericFilterResponse<ProductoResponse>> GetByFilter(GenericFilterRequest request)
		{
			GenericFilterResponse<ProductoResponse> result = _mapper
				.Map<GenericFilterResponse<ProductoResponse>>(_productoRepository
				.GetByFilter(request));

			return result;
		}

		/*public async Task<GenericFilterResponse<ProductoResponse>> GetByFilteDependingRole(GenericFilterRequest request, string userRole)
        {
            //if (request.Filtros[1].Value == "") request.Filtros[1].Value = "true";

            GenericFilterResponse<Producto> productosFiltradas = await _productoRepository
                .GetByFilteDependingRole(request, userRole);

            GenericFilterResponse<ProductoResponse> result = _mapper
                .Map<GenericFilterResponse<ProductoResponse>>(productosFiltradas);

            return result;
        }*/


		public async Task<ProductoResponse> Patch(int id, JsonPatchDocument<ProductoRequest> patchDocument)
		{
			var productoDB = _productoRepository.GetAllQueryable().FirstOrDefault(x => x.Id == id);
			var xxx = _mapper.Map<ProductoRequest>(productoDB);

			patchDocument.ApplyTo(xxx);

			_mapper.Map(xxx, productoDB);
			_productoRepository.Update(productoDB);

			ProductoResponse result = _mapper.Map<ProductoResponse>(productoDB);

			return result;
		}

		public async Task<int> LogicDelete(int id)
		{
			var result = await _productoRepository.LogicDelete(id);
			return result;
		}
		#endregion END CRUD METHODS
	}
}
