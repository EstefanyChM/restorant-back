using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using RequestResponseModel;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.JsonPatch;
using IServices;


namespace Bussnies
{
    public class CategoriaBussnies : ICategoriaBussnies
    {
        /*INYECCIÓN DE DEPENDENCIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IMapper _mapper;
        private readonly IFilesServices _filesServices;
        private readonly IProductoBussnies _productoBussnies;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaBussnies(IMapper mapper, IFilesServices filesServices, IProductoBussnies productoBussnies, ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _filesServices = filesServices;
            _productoBussnies = productoBussnies;
            _categoriaRepository = categoriaRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        string carpeta = "Categoria";

        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS
        public async Task<List<CategoriaResponse>> GetAll()
        {
            List<Categoria> categorias = await _categoriaRepository.GetAll();

            List<CategoriaResponse> lstResponse = _mapper.Map<List<CategoriaResponse>>(categorias);
            return lstResponse;
        }

        public async Task<List<CategoriaResponse>> GetAllActive()
        {
            List<Categoria> categoriasActivas = await _categoriaRepository.GetAllActive();

            List<CategoriaResponse> lstResponse = _mapper.Map<List<CategoriaResponse>>(categoriasActivas);

            return lstResponse;
        }

        public async Task<CategoriaResponse> GetById(int id, string userRole)
        {
            Categoria categoria = await _categoriaRepository.GetById(id);

            if (!categoria.Estado)
            {
                if (userRole != "Administrador") throw new Exception("Se eliminó, no hay pa ti");
            }

            CategoriaResponse result = _mapper.Map<CategoriaResponse>(categoria);
            return result;
        }


        public async Task<CategoriaResponse> Create(CategoriaRequest entity)
        {
            if (await _categoriaRepository.Existe("Nombre", entity.Nombre) != null) throw new DatoDuplicadoException(entity.Nombre);

            entity.UrlImagen = await _filesServices.SubirArchivo(
                    entity.Foto.OpenReadStream(),
                    entity.Foto.FileName,
                    carpeta
                    );

            entity.Disponibilidad = true;
            entity.Estado = true;

            Categoria categoria = _mapper.Map<Categoria>(entity);
            categoria = await _categoriaRepository.Create(categoria);
            CategoriaResponse result = _mapper.Map<CategoriaResponse>(categoria);
            return result;
        }

        public async Task<CategoriaResponse> Update(CategoriaRequest entity)
        {
            Categoria buscar = await _categoriaRepository.Existe("Nombre", entity.Nombre);

            // Primero verificar si 'buscar' es null
            if (buscar != null && buscar.Id != entity.Id)
            {
                throw new DatoDuplicadoException(entity.Nombre);
            }

            // Desvincular la entidad si ya está siendo rastreada
            //Es que ese metodo ya tiene el Detached, por eso no estoy poniendo nada acá

            if (entity.Foto != null)//Si lo que se cambió es la foto
                entity.UrlImagen = await _filesServices.SubirArchivo(
                    entity.Foto.OpenReadStream(),
                    entity.Foto.FileName,
                    carpeta
                    );

            //Quiero ver si va a cambiar la disponibilidad
            Categoria categoriaDB = await _categoriaRepository.GetById(entity.Id);

            // Desvincular la entidad si ya está siendo rastreada
            _categoriaRepository.Detach(categoriaDB);

            if (categoriaDB.Disponibilidad != entity.Disponibilidad)
            {
                //Entonces también debo cambiar la disponib de los productos
                await _productoBussnies.UpdateMultipleByCategory(entity.Id, entity.Disponibilidad);
            }


            Categoria categoria = _mapper.Map<Categoria>(entity);

            categoria = await _categoriaRepository.Update(categoria);
            CategoriaResponse result = _mapper.Map<CategoriaResponse>(categoria);
            return result;
        }

        public async Task<List<CategoriaResponse>> CreateMultiple(List<CategoriaRequest> lista)
        {
            List<Categoria> categorias = _mapper.Map<List<Categoria>>(lista);
            categorias = await _categoriaRepository.CreateMultiple(categorias);
            List<CategoriaResponse> result = _mapper.Map<List<CategoriaResponse>>(categorias);
            return result;
        }

        public async Task<List<CategoriaResponse>> UpdateMultiple(List<CategoriaRequest> lista)
        {
            List<Categoria> categorias = _mapper.Map<List<Categoria>>(lista);
            categorias = await _categoriaRepository.UpdateMultiple(categorias);
            List<CategoriaResponse> result = _mapper.Map<List<CategoriaResponse>>(categorias);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _categoriaRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<CategoriaRequest> lista)
        {
            List<Categoria> categorias = _mapper.Map<List<Categoria>>(lista);
            int cantidad = await _categoriaRepository.DeleteMultipleItems(categorias);
            return cantidad;
        }

        public async Task<GenericFilterResponse<CategoriaResponse>> GetByFilteDependingRole(GenericFilterRequest request, string userRole)
        {
            if (request.Filtros[1].Value == "") request.Filtros[1].Value = "true";

            GenericFilterResponse<Categoria> categoriasFiltradas = await _categoriaRepository
                .GetByFilteDependingRole(request, userRole);

            GenericFilterResponse<CategoriaResponse> result = _mapper
                .Map<GenericFilterResponse<CategoriaResponse>>(categoriasFiltradas);

            return result;
        }


        public async Task<CategoriaResponse> Patch(int id, JsonPatchDocument<CategoriaRequest> patchDocument)
        {
            Categoria categoriaDB = await _categoriaRepository.GetById(id);

            CategoriaRequest catDBRequest = _mapper.Map<CategoriaRequest>(categoriaDB);

            patchDocument.ApplyTo(catDBRequest); //Soy una copia de la entidad, pero con cambios
            _mapper.Map(catDBRequest, categoriaDB);//mapear los cambios rastreada por Entity Framework.

            Categoria cat = await _categoriaRepository.Update(categoriaDB);
            CategoriaResponse result = _mapper.Map<CategoriaResponse>(cat);
            return result;
        }

        public async Task<int> LogicDelete(int id)
        {
            var result = await _categoriaRepository.LogicDelete(id);
            return result;
        }




        public async Task<GenericFilterResponse<CategoriaResponse>> GetByFilter(GenericFilterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion END CRUD METHODS




    }


}
