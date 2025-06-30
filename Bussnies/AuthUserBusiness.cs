using AutoMapper;
using Azure.Core;
using BDRiccosModel;
using CommonModel;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DTO;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using RequestResponseModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Business
{
    public class AuthUserBusiness : IAuthUserBusiness
    {
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IOnlineUserRepository _onlineUserRepository;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUsuarioSistemaRepository _usuarioSistemaRepository;
        private readonly IClienteBussnies _clienteBussnies;
        private readonly IPersonaNaturalRepository _personaNaturalRepository;

        public AuthUserBusiness(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IOnlineUserRepository onlineClientRepository,
            SignInManager<ApplicationUser> signInManager,
            IUsuarioSistemaRepository usuarioSistemaRepository,
            IClienteBussnies clienteBussnies,
            IPersonaNaturalRepository personaNaturalRepository
            )
        {
            _mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
            this._onlineUserRepository = onlineClientRepository;
            this.signInManager = signInManager;
            this._usuarioSistemaRepository = usuarioSistemaRepository;
            _clienteBussnies = clienteBussnies;
            _personaNaturalRepository = personaNaturalRepository;
        }
        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR

        #region LOGIN
        public async Task<ActionResult<AutenticacionResponse>> Login(LoginDTO request, bool esPersonal)
        {
            var resultado = await signInManager.PasswordSignInAsync(
                request.Email,
                request.Password,
                isPersistent: false,
                lockoutOnFailure: false
                );

            if (resultado.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(request.Email);

                if (user.EsPersonal == esPersonal)
                {
                    string IdApplicationUser = user.Id;

                    int IdUsuario;

                    if (user.EsPersonal)
                    {
                        UsuariosSistema us = await _usuarioSistemaRepository.ObtenerUsuarioSistema(IdApplicationUser);
                        IdUsuario = us.Id;
                    }
                    else
                    {
                        OnlineUser ou = await _onlineUserRepository.ObtenerOnlineClient(IdApplicationUser);
                        IdUsuario = ou.Id;
                    }

                    AutenticacionResponse ar = await ConstruirToken(_mapper.Map<ConstruirTokenDTO>(request));
                    ar.IdUsuario = IdUsuario;

                    return ar;

                }
                else
                {
                    //return BadRequest("Login incorrecto");
                    throw new AuthenticationException("Login incorrecto");
                }

            }
            else
            {
                //return BadRequest("Login incorrecto");
                throw new AuthenticationException("Login incorrecto");
            }
        }

        #endregion lOGIN

        #region REGISTRO
        public async Task<ActionResult<AutenticacionResponse>> RegistrarOnlineClient(OnlineUserRegistrarDTO request)
        {
            var usuario = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                EsPersonal = false
            };

            var resultado = await userManager.CreateAsync(usuario, request.Password);

            if (resultado.Succeeded)
            {
                OnlineUser ou = new OnlineUser()
                {
                    IdApplicationUser = usuario.Id,
                    Estado = true,
                    Email = usuario.Email,
                };

                PersonaNaturalRequest pnr = new PersonaNaturalRequest
                {
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    NumeroDocumento = request.NumeroDocumento,
                    IdPersonaTipoDocumento = request.IdPersonaTipoDocumento,
                    Celular = request.PhoneNumber
                };
                ClienteResponse cp = await _clienteBussnies.CreatePNat(pnr);
                ou.IdCliente = cp.Id;

                await _onlineUserRepository.Create(ou);

                return await ConstruirToken(_mapper.Map<ConstruirTokenDTO>(request));
            }
            else return new BadRequestObjectResult(resultado.Errors);
        }


        public async Task<ActionResult<AutenticacionResponse>> RegistrarUsuarioSistema(UsuarioSistemaRegistrarDTO request)
        {
            PersonaNatural personaNaturalDB = await _personaNaturalRepository.GetById(request.IdPersonalEmpresa);

            if (personaNaturalDB == null) ; //Hay que implementar el return para dcir que no hay tal entidad

            ApplicationUser usuario = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = personaNaturalDB.Celular,
                EsPersonal = true,
            };

            var resultado = await userManager.CreateAsync(usuario, request.Password);

            if (resultado.Succeeded)
            {
                string rol = request.Cargo switch
                {
                    1 => "Administrador",
                    2 => "Vendedor",
                    3 => "Mozo",
                    4 => "Cocina",
                    _ => throw new ArgumentException("Cargo no válido")

                };

                await userManager.AddToRoleAsync(usuario, rol); // Asignar el rol

                UsuariosSistema us = new UsuariosSistema()
                {
                    IdPersonalEmpresa = request.IdPersonalEmpresa,
                    IdApplicationUser = usuario.Id,
                    Email = request.Email,
                    Estado = true,
                };

                await _usuarioSistemaRepository.Create(us);

                //SI NO ES AWAIT, DA PROBLEMAS DE THREADS
                AutenticacionResponse token = await ConstruirToken(_mapper.Map<ConstruirTokenDTO>(request));

                string cargo = request.Cargo switch
                {
                    1 => "EsAdmin",
                    2 => "EsVendedor",
                    3 => "EsMozo",
                    4 => "EsCocina",
                    _ => throw new ArgumentException("Cargo no válido") // Manejo de casos no esperados
                };

                await userManager.AddClaimAsync(usuario, new Claim(cargo, request.Cargo.ToString()));

                return token;
            }
            else return new BadRequestObjectResult(resultado.Errors);
        }
        #endregion REGISTRO

        #region METODOS
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO request)
        {
            var usuario = await userManager.FindByEmailAsync(request.Email);
            await userManager.RemoveClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return new NoContentResult();
        }
        #endregion METODOS


        #region MÉTODOS PARA EL TOKEN
        private async Task<AutenticacionResponse>

        ConstruirToken(ConstruirTokenDTO request)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", request.Email),
                //new Claim("lo que yo quiera", "cualquier otro valor")
            };

            var usuario = await userManager.FindByEmailAsync(request.Email);

            var claimsDB = await userManager.GetClaimsAsync(usuario); //jala todos los claims del usuario en la DB

            claims.AddRange(claimsDB);

            // Obtener los roles del usuario
            var roles = await userManager.GetRolesAsync(usuario);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); //LA LLAVE ES ESTÁNDAR , A PESAR DE SER MUY LARGA
                claims.Add(new Claim("role", role));
            }

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new AutenticacionResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
                EsPersonal = usuario.EsPersonal
            };
        }

        public async Task<ActionResult> AsignarRol(AsignarRolDTO request)
        {
            var usuario = await userManager.FindByEmailAsync(request.Email);

            if (usuario == null)
            {
                return new NotFoundObjectResult("Usuario no encontrado");
            }

            var resultado = await userManager.AddToRoleAsync(usuario, request.Rol);

            if (resultado.Succeeded)
            {
                return new OkObjectResult($"Rol {request.Rol} asignado correctamente a {request.Email}");
            }
            else
            {
                return new BadRequestObjectResult(resultado.Errors);
            }
        }


        /****************************************/

        public async Task<ActionResult<AutenticacionResponse>> RenovarToken(string email)
        {
            var construirToken = new ConstruirTokenDTO()
            {
                Email = email
            };

            return await ConstruirToken(construirToken);
        }



        #endregion MÉTODOS PARA EL TOKEN

    }
}
