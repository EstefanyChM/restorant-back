using ApiWeb;
using ApiWeb.Midleware;
using BDRiccosModel;
using Business;
using Bussnies;
using IBussnies;
using IRepository;
using IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Repository;
using Services;
using System.Reflection;
using System.Text;
using UtilAutomapper;
using Serilog;
using Newtonsoft.Json;
using MercadoPago.Config;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();


var builder = WebApplication.CreateBuilder(args);

// Configurar la cultura para usar punto como separador decimal (en-US)
var cultureInfo = new CultureInfo("en-US");  // Cultura que usa el punto como separador decimal
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
//builder.Services.AddSignalR();

// CONFIGURACI�N DE JSON
builder.Services
	.AddControllers()
	.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles)
	.AddNewtonsoftJson(options =>
		{
			options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		});



builder.Services.AddDbContext<_dbRiccosContext>();


QuestPDF.Settings.License = LicenseType.Community;

#region Inyección de dependencias para business y repository

//INYECCI�N DE DEPENDENCIAS ENTRE Interfaz y su implementaci�n
builder.Services.AddScoped<IChatbotWebhookBusiness, ChatbotWebhookBusiness>();
//jejej prometo voy a llamr a los otros busines
/*******************************/
builder.Services.AddScoped<IAuthUserBusiness, AuthUserBusiness>();
builder.Services.AddScoped<IOnlineUserBusiness, OnlineUserBusiness>();

builder.Services.AddScoped<IPersonaNaturalRepository, PersonaNaturalRepository>();
builder.Services.AddScoped<IPersonaNaturalBussnies, PersonaNaturalBussnies>();

builder.Services.AddScoped<IPersonaJuridicaBussnies, PersonaJuridicaBussnies>();
builder.Services.AddScoped<IPersonaJuridicaRepository, PersonaJuridicaRepository>();


builder.Services.AddScoped<IPersonalEmpresaRepository, PersonalEmpresaRepository>();
builder.Services.AddScoped<IProductoBussnies, ProductoBussnies>();

builder.Services.AddScoped<ICategoriaBussnies, CategoriaBussnies>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IClienteBussnies, ClienteBussnies>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IPersonalEmpresaBusiness, PersonalEmpresaBusiness>();
builder.Services.AddScoped<IPersonalEmpresaRepository, PersonalEmpresaRepository>();



builder.Services.AddScoped<IMensajeContactoRepository, MensajeContactoRepository>();
builder.Services.AddScoped<IMensajeContactoBussnies, MensajeContactoBussnies>();


builder.Services.AddScoped<IOnlineUserRepository, OnlineUserRepository>();

builder.Services.AddScoped<IUsuarioSistemaRepository, UsuarioSistemaRepository>();
builder.Services.AddScoped<IUsuarioSistemaBusiness, UsuarioSistemaBusiness>();

builder.Services.AddScoped<IEmpresaBusiness, EmpresaBusiness>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();

builder.Services.AddScoped<IEmailSuscriptorBusiness, EmailSuscriptorBusiness>();
builder.Services.AddScoped<IEmailSuscriptorRepository, EmailSuscriptorRepository>();

builder.Services.AddScoped<IMesaBussnies, MesaBusiness>();
builder.Services.AddScoped<IMesaRepository, MesaRepository>();

builder.Services.AddScoped<IEnTiendaBussnies, EnTiendaBussnies>();
builder.Services.AddScoped<IEnTiendaRepository, EnTiendaRepository>();

builder.Services.AddScoped<IPromocionBussnies, PromocionBussnies>();
builder.Services.AddScoped<IPromocionRepository, PromocionRepository>();

builder.Services.AddScoped<IDeliveryBussnies, DeliveryBussnies>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

builder.Services.AddScoped<IPickupBussnies, PickupBussnies>();
builder.Services.AddScoped<IPickupRepository, PickupRepository>();



builder.Services.AddScoped<IPedidoBussnies, PedidoBussnies>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped<IMensajeBusiness, MensajeBusiness>();

builder.Services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();



builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IVentaBussnies, VentaBussnies>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

// Para AsuntoMensaje
builder.Services.AddScoped<IAsuntoMensajeBussnies, AsuntoMensajeBussnies>();
builder.Services.AddScoped<IAsuntoMensajeRepository, AsuntoMensajeRepository>();

// Para Servicios
builder.Services.AddScoped<IServiciosBussnies, ServiciosBussnies>();
builder.Services.AddScoped<IServiciosRepository, ServiciosRepository>();

// Para HorarioAtencion
builder.Services.AddScoped<IHorarioAtencionBusiness, HorarioAtencionBusiness>();
builder.Services.AddScoped<IHorarioAtencionRepository, HorarioAtencionRepository>();



//INYECCI�N DE DEPENDENCIAS PARA LOS SERVICIOS

builder.Services.AddTransient<IServicioEmailSendGrid, ServicioEmailSendGrid>();

builder.Services.AddScoped<IFilesServices, FilesServices>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<IWebSocketService, WebSocketService>();

builder.Services.AddSingleton<IApisPeruServices, ApisPeruServices>();



#endregion inyección de dependencias



// MODIFICANDO LAS RESTRICCIONES DEL PASSWORD DE IDENTITY
//OOOH, QUE OTRO LO HAGA (?

// CONFIGURACI�N DEL CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin();
		builder.AllowAnyMethod(); // GET, POST, PUT, DELETE, PATCH
		builder.AllowAnyHeader();
	});
});




// JWT IMPLEMENTACI�N
builder.Services
	.AddHttpContextAccessor()
	.AddAuthorization()
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			//IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"])),
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"])),
			RoleClaimType = ClaimTypes.Role, // Indicar que los roles vienen con esta clave

			ClockSkew = TimeSpan.Zero,

		};
	});



// CONFIGURACI�N DE SWAGGER
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{

		Title = "WebAPIRiccos ",
		Version = "v1",
		Description = "APIs para Ricco's",
		Contact = new OpenApiContact
		{
			Name = "xxxxx",
			Email = "yyyyy",
			Url = new Uri("https://www.linkedin.com"),
		},
	});

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{}
		}
	});

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});



//CONFIGURANDO PARA AUTORIZACI�N BASADA EN CLAIMS
/*builder.Services.AddAuthorization(opciones =>
{

    opciones.AddPolicy("EsAdmin", politica => politica.RequireClaim("esAdmin"));

    //opciones.AddPolicy("EsVendedor", politica => politica.RequireClaim("esVendedor"));
});*/


builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("EsAdmin", policy => policy.RequireRole("Administrador"));
	options.AddPolicy("EsVendedor", policy => policy.RequireRole("Vendedor"));
	options.AddPolicy("EsMozo", policy => policy.RequireRole("Mozo"));
	options.AddPolicy("EsCocinero", policy => policy.RequireRole("Cocina"));
});





// CONFIGURACI�N DE AUTOMAPPER
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(AutoMapperProfiles).Assembly);


builder.Services
	.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<_dbRiccosContext>()
	.AddDefaultTokenProviders();

builder.Services.AddApplicationInsightsTelemetry();


// Configuración del SDK de Mercado Pago
MercadoPagoConfig.AccessToken = builder.Configuration["MercadoPago:AccessToken"];





var app = builder.Build();

/*********** QUIERO ROLES!!! *************************/
// Configure the database and seed roles
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DataSeeder.InitializeAsync(userManager, roleManager);
}*/
/*******************************************/







// CONFIGURACI�N DEL PIPELINE DE HTTP
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.Use(async (context, next) =>
{
	Console.WriteLine(" Middleware de autenticación ejecutándose...");
	var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

	if (authHeader != null)
	{
		Console.WriteLine($" Token recibido: {authHeader}");
	}
	else
	{
		Console.WriteLine(" No se recibió token");
	}

	await next();
	Console.WriteLine($"📢 Estado de la respuesta: {context.Response.StatusCode}");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware(typeof(ApiMiddleware));

//app.MapHub<NotificationHub>("/notificationhub");



app.MapControllers();

app.Run();
