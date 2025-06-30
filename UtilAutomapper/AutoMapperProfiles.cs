using AutoMapper;
using BDRiccosModel;
using RequestResponseModel;
using DTO;
using CommonModel;

namespace UtilAutomapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PersonaNatural, PersonaNaturalRequest>()
    .ReverseMap()
    .ForMember(dest => dest.IdPersonaTipoDocumento, opt => opt.MapFrom(src => (short)1));



            CreateMap<PersonaNatural, PersonaNaturalResponse>()
                .ForMember(dest => dest.IdPersonaNatural, opt => opt.MapFrom(src => src.Id));
            CreateMap<PersonaNaturalRequest, PersonaNaturalResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<PersonaNaturalResponse>, GenericFilterResponse<PersonaNatural>>().ReverseMap();
            CreateMap<ApisPeruPersonaResponse, PersonaNatural>().ReverseMap();
            CreateMap<PersonaNaturalRequest, PersonalEmpresaRequest>().ReverseMap();



            CreateMap<PersonaJuridica, PersonaJuridicaRequest>()
    .ReverseMap()
    .ForMember(dest => dest.IdPersonaTipoDocumento, opt => opt.MapFrom(src => (short)2));

            CreateMap<PersonaJuridica, PersonaJuridicaResponse>()
                .ForMember(dest => dest.IdPersonaJuridica, opt => opt.MapFrom(src => src.Id));
            CreateMap<PersonaJuridicaRequest, PersonaJuridicaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<PersonaJuridicaResponse>, GenericFilterResponse<PersonaJuridica>>().ReverseMap();
            CreateMap<ApisPeruPersonaResponse, PersonaJuridica>().ReverseMap();


            CreateMap<Promocion, PromocionRequest>().ReverseMap();
            CreateMap<Promocion, PromocionResponse>().ReverseMap();
            CreateMap<PromocionRequest, PromocionResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<PromocionResponse>, GenericFilterResponse<Promocion>>().ReverseMap();
            CreateMap<DetallesPromocion, DetallesPromocionRequest>().ReverseMap();



            CreateMap<Cliente, ClienteRequest>().ReverseMap();
            CreateMap<Cliente, ClienteResponse>().ReverseMap();
            CreateMap<ClienteRequest, ClienteResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<ClienteResponse>, GenericFilterResponse<Cliente>>().ReverseMap();
            CreateMap<ClienteRequest, PersonaNatural>().ReverseMap()
                .ForMember(dest => dest.IdTablaPersonaNatural, opt => opt.MapFrom(src => src.Id))
                ;
            CreateMap<ClienteRequest, PersonaJuridica>().ReverseMap()
                .ForMember(dest => dest.IdTablaPersonaNatural, opt => opt.MapFrom(src => src.Id))
                ;
            CreateMap<ClienteResponse, PersonaNatural>().ReverseMap();
            CreateMap<ClienteResponse, PersonaJuridica>().ReverseMap();





            CreateMap<MetodoPago, MetodoPagoRequest>().ReverseMap();
            CreateMap<MetodoPago, MetodoPagoResponse>().ReverseMap();
            CreateMap<MetodoPagoRequest, MetodoPagoResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<MetodoPagoResponse>, GenericFilterResponse<MetodoPago>>().ReverseMap();


            CreateMap<Categoria, CategoriaRequest>().ReverseMap();
            CreateMap<Categoria, CategoriaResponse>()
                .ForMember(dest => dest.CantidadDeProductos, opt => opt.MapFrom(src => src.Productos.Count))
                .ForMember(dest => dest.PrecioMinimo, opt => opt.MapFrom(src => src.Productos.Any() ? src.Productos.Min(p => p.Precio) : (decimal?)null))
                .ForMember(dest => dest.PrecioMaximo, opt => opt.MapFrom(src => src.Productos.Any() ? src.Productos.Max(p => p.Precio) : (decimal?)null))
                .ReverseMap();
            CreateMap<CategoriaRequest, CategoriaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<CategoriaResponse>, GenericFilterResponse<Categoria>>()
                .ReverseMap();
            CreateMap<CategoriaRequest, CategoriaCrearDTO>().ReverseMap();


            CreateMap<Producto, ProductoRequest>().ReverseMap();
            CreateMap<Producto, ProductoResponse>()
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.IdCategoriaNavigation.Nombre))
            .ReverseMap();
            CreateMap<ProductoRequest, ProductoResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<ProductoResponse>, GenericFilterResponse<Producto>>().ReverseMap();




            CreateMap<MensajeContacto, MensajeContactoRequest>().ReverseMap();
            CreateMap<MensajeContacto, MensajeContactoResponse>()
                .ForMember(dest => dest.AsuntoNombre, opt => opt.MapFrom(src => src.IdAsuntoNavigation.Nombre))
                .ForMember(dest => dest.RemiteEmail, opt => opt.MapFrom(src => src.IdRemiteNavigation.Email))
                .ForMember(dest => dest.MensajeEstado, opt => opt.MapFrom(src => src.IdEstadoMensajeNavigation.Nombre))
                .ReverseMap();
            CreateMap<MensajeContactoRequest, MensajeContactoResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<MensajeContactoResponse>, GenericFilterResponse<MensajeContacto>>().ReverseMap();
            CreateMap<MensajeContacto, MensajeCrearDTO>().ReverseMap();

            CreateMap<MensajeContactoRequest, MensajeCrearDTO>().ReverseMap();
            CreateMap<Remite, MensajeCrearDTO>().ReverseMap();


            CreateMap<Venta, VentaRequest>().ReverseMap();
            CreateMap<Venta, VentaResponse>()
                .ForMember(dest => dest.PedidoResponseNav, opt => opt.MapFrom(src => src.IdPedidoNavigation))
                .ReverseMap();
            CreateMap<VentaRequest, VentaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<VentaResponse>, GenericFilterResponse<Venta>>().ReverseMap();
            CreateMap<VistaVenta, VentaResponse>().ReverseMap();

            CreateMap<Empresa, EmpresaRequest>().ReverseMap();
            CreateMap<Empresa, EmpresaResponse>().ReverseMap();
            CreateMap<EmpresaRequest, EmpresaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<EmpresaResponse>, GenericFilterResponse<Empresa>>().ReverseMap();

            CreateMap<EmailSuscriptor, EmailSuscriptorRequest>().ReverseMap();
            CreateMap<EmailSuscriptor, EmailSuscriptorResponse>().ReverseMap();
            CreateMap<EmailSuscriptorRequest, EmailSuscriptorResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<EmailSuscriptorResponse>, GenericFilterResponse<EmailSuscriptor>>().ReverseMap();
            CreateMap<EmailSubscriptorDTO, EmailSuscriptorRequest>().ReverseMap();


            CreateMap<MesaRequest, Mesa>().ReverseMap();
            CreateMap<Mesa, MesaResponse>().ReverseMap();
            CreateMap<MesaRequest, MesaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<MesaResponse>, GenericFilterResponse<Mesa>>().ReverseMap();



            CreateMap<EnTiendaRequest, EnTienda>().ReverseMap();
            CreateMap<EnTienda, EnTiendaResponse>()
                .ForMember(dest => dest.Finalizado, opt => opt.MapFrom(src => src.IdPedidoNavigation.Finalizado))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.IdPedidoNavigation.Total))
                .ReverseMap();
            CreateMap<EnTiendaRequest, EnTiendaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<EnTiendaResponse>, GenericFilterResponse<EnTienda>>().ReverseMap();


            // Mapeo entre PedidoRequest y Pedido
            CreateMap<PedidoRequest, Pedido>()
                .ForMember(dest => dest.DetallePedidos, opt => opt.MapFrom(src => src.DetallePedidosRequest))
                .ReverseMap();
            CreateMap<Pedido, PedidoResponse>()
                .ForMember(dest => dest.DetallePedidoResp, opt => opt.MapFrom(src => src.DetallePedidos))
                .ReverseMap();
            CreateMap<PedidoRequest, PedidoResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<PedidoResponse>, GenericFilterResponse<Pedido>>().ReverseMap();
            /******************************************/
            CreateMap<DetallePedidoRequest, DetallePedido>().ReverseMap();
            CreateMap<DetallePedido, DetallePedidoResponse>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.Nombre))
                .ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.IdProductoNavigation.IdCategoriaNavigation.Nombre))
                .ReverseMap();



            CreateMap<OnlineUser, OnlineUserRequest>().ReverseMap();
            CreateMap<OnlineUser, OnlineUserResponse>().ReverseMap();
            CreateMap<OnlineUserRequest, OnlineUserResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<OnlineUser>, GenericFilterResponse<OnlineUser>>().ReverseMap();
            CreateMap<OnlineUser, OnlineUserRegistrarDTO>().ReverseMap();
            CreateMap<PersonaNatural, OnlineUserRegistrarDTO>().ReverseMap();



            CreateMap<PersonalEmpresa, PersonalEmpresaRequest>().ReverseMap();
            CreateMap<PersonalEmpresaRequest, PersonalEmpresaResponse>().ReverseMap();


            CreateMap<GenericFilterResponse<PersonalEmpresa>, GenericFilterResponse<PersonalEmpresa>>().ReverseMap();
            CreateMap<PersonalEmpresa, UsuarioSistemaRegistrarDTO>().ReverseMap();
            CreateMap<PersonaNatural, UsuarioSistemaRegistrarDTO>().ReverseMap();
            CreateMap<PersonaNatural, PersonalEmpresaRequest>().ReverseMap();
            CreateMap<PersonaNatural, PersonalEmpresaCreateDTO>().ReverseMap();

            CreateMap<PersonaNatural, PersonalEmpresaResponse>()
                .ForMember(dest => dest.IdPersonaNatural, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            // NO ME GUSTA, PIENSO ES MEJOR CREAR EL OBJETO CON LA NUEVA CLASE CON NEW {}, PERO QUIZÁ MÁS ADELANTE VEA UNA VENTAJA, HABRÁ QUE VER

            //COMO LO VOY A  USAR EN DIFERENTES FUNCIONES, SÍ VALE, O TENDRÍA QUE HACER LA CONVERSIÓN EN CADA UNA DE ELLAS
            CreateMap<PersonalEmpresa, PersonalEmpresaResponse>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.IdPersonaNavigation.Nombre))
                .ForMember(dest => dest.IdPersonaNatural, opt => opt.MapFrom(src => src.IdPersonaNavigation.Id))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.IdPersonaNavigation.Apellidos))
                .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.IdPersonaNavigation.FechaNacimiento))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.IdPersonaNavigation.Celular))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.IdPersonaNavigation.Direccion))
                .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.IdPersonaNavigation.NumeroDocumento))
                .ForMember(dest => dest.IdPersonaTipoDocumento, opt => opt.MapFrom(src => src.IdPersonaNavigation.IdPersonaTipoDocumento))
                .ReverseMap();

            CreateMap<UsuariosSistema, UsuariosSistemaRequest>().ReverseMap();
            CreateMap<UsuariosSistema, UsuariosSistemaResponse>()
                .ForMember(dest => dest.IdPersonaNatural, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IdApplicationUserNavigation.Email))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.Nombre))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.Apellidos))
                .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.FechaNacimiento))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.Celular))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.Direccion))
                .ForMember(dest => dest.NumeroDocumento, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.NumeroDocumento))
                .ForMember(dest => dest.IdPersonaTipoDocumento, opt => opt.MapFrom(src => src.IdPersonalEmpresaNavigation.IdPersonaNavigation.IdPersonaTipoDocumento))

                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.IdApplicationUserNavigation.UserRoles.Select(ur => ur.Role.Name)
                ))
                .ReverseMap();

            CreateMap<UsuariosSistemaRequest, UsuariosSistemaResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<MesaResponse>, GenericFilterResponse<UsuariosSistema>>().ReverseMap();


            // Para AsuntoMensaje
            CreateMap<Asunto, AsuntoMensajeRequest>().ReverseMap();
            CreateMap<Asunto, AsuntoMensajeResponse>().ReverseMap();
            CreateMap<AsuntoMensajeRequest, AsuntoMensajeResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<AsuntoMensajeResponse>, GenericFilterResponse<Asunto>>().ReverseMap();

            // Para Servicios
            CreateMap<Service, ServiciosRequest>().ReverseMap();
            CreateMap<Service, ServiciosResponse>().ReverseMap();
            CreateMap<ServiciosRequest, ServiciosResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<ServiciosResponse>, GenericFilterResponse<Service>>().ReverseMap();

            // Para HorarioAtencion
            CreateMap<HorariosRegulares, HorarioAtencionRequest>().ReverseMap();
            CreateMap<HorariosRegulares, HorarioAtencionResponse>().ReverseMap();
            CreateMap<HorarioAtencionRequest, HorarioAtencionResponse>().ReverseMap();
            CreateMap<GenericFilterResponse<HorarioAtencionResponse>, GenericFilterResponse<HorariosRegulares>>().ReverseMap();



            /*** PARA ENTIDADES QUE CREAN MÁS DE 1 TABLA  ***/




            /********** PARA CREAR TOKEN **********/
            CreateMap<ConstruirTokenDTO, OnlineUserRegistrarDTO>().ReverseMap();
            CreateMap<ConstruirTokenDTO, LoginDTO>().ReverseMap();

            CreateMap<ConstruirTokenDTO, UsuarioSistemaRegistrarDTO>().ReverseMap();
            CreateMap<ConstruirTokenDTO, PersonalEmpresaLoginDTO>().ReverseMap();



            /********** PARA CONVERTIR LA PROMO EN DETALLES PEDIDO **********/

            CreateMap<DetallePedidoRequest, DetallesPromocion>().ReverseMap();

        }
    }
}