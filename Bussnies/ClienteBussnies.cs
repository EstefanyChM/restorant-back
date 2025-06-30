using System.Xml.XPath;
using AutoMapper;
using Azure.Core;
using BDRiccosModel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DTO;
using IBussnies;
using IRepository;
using IServices;
using Repository;
using RequestResponseModel;
using Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ExcepcionesPersonalizadas;
using Microsoft.EntityFrameworkCore;
using CommonModel;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Bussnies
{
    public class ClienteBussnies : IClienteBussnies
    {
        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IMapper _mapper;
        private readonly IPersonaJuridicaRepository _personaJuridicaRepository;
        private readonly IPersonaNaturalRepository _personaNaturalRepository;
        private readonly IClienteRepository _ClienteRepository;

        private readonly IApisPeruServices _apisPeruServices;
        public ClienteBussnies(IMapper mapper, IApisPeruServices apisPeruServices)
        {
            _mapper = mapper;

            _apisPeruServices = apisPeruServices;

            _ClienteRepository = new ClienteRepository();
            _personaJuridicaRepository = new PersonaJuridicaRepository();
            _personaNaturalRepository = new PersonaNaturalRepository();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS
        public async Task<List<ClienteResponse>> GetAll()
        {
            List<Cliente> VClientes = await _ClienteRepository.GetAll();
            List<ClienteResponse> lstResponse = _mapper.Map<List<ClienteResponse>>(VClientes);
            return lstResponse;
        }


        public async Task<ClienteResponse> GetById(int id)
        {
            Cliente Cliente = await _ClienteRepository.GetById(id);
            ClienteResponse resul = _mapper.Map<ClienteResponse>(Cliente);
            return resul;
        }

        public async Task<ClienteResponse> CreatePNat(PersonaNaturalRequest entity) //HAY QUE REDUCIR DE SER POSIBLE
        {
            // Buscar si ya existe una PersonaNatural con el mismo número de documento
            var personaNaturalDB = await _personaNaturalRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync(x => x.NumeroDocumento == entity.NumeroDocumento);

            if (personaNaturalDB != null)
            {
                // Buscar si la PersonaNatural ya está en Cliente
                var clienteDB = await _ClienteRepository
                    .GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.IdTablaPersonaNatural == personaNaturalDB.Id);

                if (clienteDB != null)
                {
                    var response = _mapper.Map<ClienteResponse>(personaNaturalDB);
                    _mapper.Map(clienteDB, response); // Agregar datos de PersonalEmpresa
                    return response;
                }
                else
                {
                    // Si no existe en Clietne, crearla
                    return await CrearCliente(personaNaturalDB, personaNaturalDB.Id, entity.IdPersonaTipoDocumento, true);
                }
            }
            else
            {
                // Si no existe, primero crear la PersonaNatural
                var nuevaPersona = await _personaNaturalRepository.Create(_mapper.Map<PersonaNatural>(entity));

                // Luego, crear Cliente
                return await CrearCliente(nuevaPersona, nuevaPersona.Id, entity.IdPersonaTipoDocumento, true);

            }
        }

        public async Task<ClienteResponse> CrearCliente(object personaNatural, int id, short idPersonaTipoDoc, bool EsPN)
        {
            Cliente cliente = new Cliente
            {
                Estado = true,
                IdTipoPersona = idPersonaTipoDoc,
                IdTablaPersonaNatural = EsPN ? id : null,
                IdTablaPersonaJuridica = EsPN ? null : id
            };


            await _ClienteRepository.Create(cliente);
            // Mapeo combinado de PersonaNatural y Clñiente
            var response = _mapper.Map<ClienteResponse>(personaNatural);
            _mapper.Map(cliente, response); // Agregar datos de Cliente

            return response;
        }




        public async Task<ClienteResponse> CreatePJur(PersonaJuridicaRequest entity)
        {
            // Buscar si ya existe una PersonaJuridica con el mismo número de documento
            var personaJuridicaDB = await _personaJuridicaRepository
                .GetAllQueryable()
                .FirstOrDefaultAsync(x => x.Ruc == entity.Ruc);

            if (personaJuridicaDB != null)
            {
                // Buscar si la PersonaNatural ya está en Cliente
                var clienteDB = await _ClienteRepository
                    .GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.IdTablaPersonaJuridica == personaJuridicaDB.Id);

                if (clienteDB != null)
                {
                    var response = _mapper.Map<ClienteResponse>(personaJuridicaDB);
                    _mapper.Map(clienteDB, response); // Agregar datos de PersonalEmpresa
                    return response;
                }
                else
                {
                    // Si no existe en Clietne, crearla
                    return await CrearCliente(personaJuridicaDB, personaJuridicaDB.Id, 2, false);
                }
            }
            else
            {
                // Si no existe, primero crear la PersonaJuridica
                var nuevaPersona = await _personaJuridicaRepository.Create(_mapper.Map<PersonaJuridica>(entity));

                // Luego, crear Cliente
                return await CrearCliente(nuevaPersona, nuevaPersona.Id, 2, false);

            }
        }


        public async Task<ClienteResponse> Create(ClienteRequest entity)
        {
            entity.Estado = true;
            Cliente cliente = await _ClienteRepository.Create(_mapper.Map<Cliente>(entity));
            return _mapper.Map<ClienteResponse>(cliente);
        }

        public async Task<List<ClienteResponse>> CreateMultiple(List<ClienteRequest> lista)
        {
            List<Cliente> Clientes = _mapper.Map<List<Cliente>>(lista);
            Clientes = await _ClienteRepository.CreateMultiple(Clientes);
            List<ClienteResponse> result = _mapper.Map<List<ClienteResponse>>(Clientes);
            return result;
        }

        public async Task<ClienteResponse> Update(ClienteRequest entity)
        {
            /*int? idPersona = _ClienteRepository.GetIdPersona(entity.Id);

			PersonaNaturalRequest personaEntity = new PersonaNaturalRequest
			{
				Id = idPersona.HasValue ? idPersona.Value : 0,
				IdPersonaTipoDocumento = entity.IdPersonaTipoDocumento,
				NumeroDocumento = entity.NumeroDocumento,
				Nombre = entity.Nombre,
				Apellidos = entity.Apellidos,
				FechaNacimiento = entity.FechaNacimiento,
				Email = entity.Email,
				Celular = entity.Celular,
				Direccion = entity.Direccion,
				
			};

			ClienteRequest clienteEntity = new ClienteRequest
			{
				Id = entity.Id,
			};

			PersonaNatural persona = _mapper.Map<PersonaNatural>(personaEntity);
			persona = await _PersonaRepository.Update(persona);

			VCliente vCliente = _ClienteRepository.GetByIdClienteVCliente(entity.Id);


			// Realizar la asignación de valores de VCliente a ClienteResponse
			ClienteResponse result = new ClienteResponse
			{
				Id = vCliente.IdCliente,
				IdPersona = vCliente.IdPersona,
				Nombre = vCliente.Nombre,
				Apellidos = vCliente.Apellidos,
				FechaNacimiento = vCliente.FechaNacimiento,
				Email = vCliente.Email,
				Celular = vCliente.Celular,
				IdPersonaTipoDocumento = vCliente.IdPersonaTipoDocumento,
				//Estado = vCliente.Estado
			};

			return result;*/
            throw new Exception();
        }



        public async Task<List<ClienteResponse>> UpdateMultiple(List<ClienteRequest> lista)
        {
            List<Cliente> Clientes = _mapper.Map<List<Cliente>>(lista);
            Clientes = await _ClienteRepository.UpdateMultiple(Clientes);
            List<ClienteResponse> result = _mapper.Map<List<ClienteResponse>>(Clientes);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _ClienteRepository.Delete(id);
            return cantidad;
        }

        public int Update(int id)
        {
            int cantidad = _ClienteRepository.Update(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<ClienteRequest> lista)
        {
            List<Cliente> Clientes = _mapper.Map<List<Cliente>>(lista);
            int cantidad = await _ClienteRepository.DeleteMultipleItems(Clientes);
            return cantidad;
        }




        public GenericFilterResponse<VCliente> FiltradoDeVCliente(GenericFilterRequest request)
        {
            // Recupera los datos usando el método existente
            var vClienteResponse = _ClienteRepository.GetByFilterVCliente(request);

            // Mapea VCliente a ClienteResponse usando AutoMapper
            var clienteResponseList = _mapper.Map<List<VCliente>>(vClienteResponse.Lista);

            // Crea un nuevo GenericFilterResponse<ClienteResponse>
            var clienteResponse = new GenericFilterResponse<VCliente>
            {
                TotalRegistros = vClienteResponse.TotalRegistros,
                Lista = clienteResponseList
            };

            return clienteResponse;
        }

        public GenericFilterResponse<VCliente> GetByFilterVCliente(GenericFilterRequest request)
        {
            var vClienteResponse = _ClienteRepository.GetByFilterVCliente(request);

            // Mapea VCliente a ClienteResponse usando AutoMapper
            var clienteResponseList = _mapper.Map<List<VCliente>>(vClienteResponse.Lista);

            // Crea un nuevo GenericFilterResponse<ClienteResponse>
            var clienteResponse = new GenericFilterResponse<VCliente>
            {
                TotalRegistros = vClienteResponse.TotalRegistros,
                Lista = clienteResponseList
            };

            return clienteResponse;
        }

        public async Task<GenericFilterResponse<ClienteResponse>> GetByFilter(GenericFilterRequest request)
        {
            GenericFilterResponse<ClienteResponse> result = _mapper.Map<GenericFilterResponse<ClienteResponse>>(_ClienteRepository.GetByFilter(request));

            return result;

        }

        /*public GenericFilterResponse<ClienteResponse> GetByFilterVCliente(GenericFilterRequest request)
		{
			GenericFilterResponse<ClienteResponse> result = _mapper.Map<GenericFilterResponse<ClienteResponse>>(_ClienteRepository.GetByFilterVCliente(request));

			return result;
		}*/


        public List<VCliente> ObtenerVistaCliente()
        {
            // Aquí puedes realizar cualquier lógica adicional si es necesario
            var filterRequest = new GenericFilterRequest(); // Puedes crear un filtro si lo necesitas

            // Llamada al repositorio para obtener los datos
            var filterResponse = _ClienteRepository.GetByFilterVCliente(filterRequest);

            // Retorna la lista de VClientes obtenida del repositorio
            return filterResponse.Lista;
        }


        #endregion END CRUD METHODS

        public VPersona GetByTipoNroDocumento(string tipoDocumento, string NroDocumento)
        {
            /*
			 * VPersona vPersona = _PersonaRepository.GetByTipoNroDocumento(tipoDocumento, NroDocumento);

			if (vPersona == null || vPersona.Id == 0)
			{
				if (tipoDocumento.ToLower() == "dni")
				{
					ApisPeruPersonaResponse pres = _apisPeruServices.PersonaPorDNI(NroDocumento);
					if (pres.Success)
					{
						vPersona = new VPersona();
						vPersona.NroDocumento = pres.dni;
						vPersona.ApellidoMaterno = pres.ApellidoMaterno;
						vPersona.ApellidoPaterno = pres.ApellidoPaterno;
						vPersona.Nombre = pres.Nombre;
					}
				}
				else
				{
					ApisPeruEmpresaResponse pres = _apisPeruServices.EmpresaPorRUC(NroDocumento);
					//
				}


				//tengo que consumir el web service de APIS Peru
			}
			return vPersona;*/
            throw new NotImplementedException();

        }

        public List<VCliente> GetVClientActive()
        {
            throw new NotImplementedException();
        }

        public Task<int> LogicDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ClienteResponse> Patch(int id, JsonPatchDocument<ClienteRequest> patchDocument)
        {
            throw new NotImplementedException();
        }


    }
}