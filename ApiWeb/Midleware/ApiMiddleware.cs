using AutoMapper;
using CommonModel;
using DocumentFormat.OpenXml.Vml.Office;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RequestResponseModel;
using System.Net;


namespace ApiWeb.Midleware
{
    public class ApiMiddleware
    {

        private readonly RequestDelegate next;
        private readonly IHelperHttpContext _helperHttpContext = null;
        private readonly IMapper _mapper;
        private readonly ILogger<ApiMiddleware> _logger;
        //private readonly IErrorBussnies _errorBussnies;


        public ApiMiddleware(RequestDelegate next, IMapper mapper, ILogger<ApiMiddleware> logger)
        {
            this.next = next;
            _helperHttpContext = new HelperHttpContext();
            _mapper = mapper;
            _logger = logger;
            //_errorBussnies = new ErrorBussnies(mapper);
        }




        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();

                // Verificar si el usuario está autenticado
                if (context.User.Identity.IsAuthenticated)
                {
                    // Obtener el UserId desde los claims de Identity
                    var userId = context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        _logger.LogInformation($"El ID del usuario es: {userId}");
                    }
                    else
                    {
                        _logger.LogWarning("No se pudo obtener el UserId del usuario autenticado.");
                    }
                }
                else
                {
                    _logger.LogWarning("Usuario no autenticado.");
                }

                await next(context);
            }
            catch (SqlException ex)
            {
                _logger.LogError($"ERROR 001 : {ex}");
                CustomException exx = new CustomException("001", "Error en base de datos");
                await HandleExceptionAsync(context, exx);
            }
            catch (DbUpdateException ex)
            {
                CustomException exx = new CustomException("002", "Error al actualizar registros");
                await HandleExceptionAsync(context, exx);
            }
            catch (DivideByZeroException ex)
            {
                CustomException exx = new CustomException("003", "Error de división entre 0");
                await HandleExceptionAsync(context, exx);
            }
            catch (ArithmeticException ex)
            {
                CustomException exx = new CustomException("004", "Error al hacer algún cálculo");
                await HandleExceptionAsync(context, exx);
            }
            catch (DatoDuplicadoException ex)
            {
                CustomException exx = new CustomException("006", $"Ya existe {ex.Message}");
                await HandleExceptionAsync(context, exx);
            }

            catch (ResourceNotFoundException ex) // Personaliza tu excepción para recursos no encontrados
            {
                CustomException exx = new CustomException("404", "Recurso no encontrado");
                await HandleExceptionAsync(context, exx);
            }

            catch (CategoriaNoDisponibleException ex)
            {
                // Log de la excepción con información adicional
                _logger.LogError($"Excepción capturada: {ex.Message}");

                // Crear una instancia de CustomException con el mensaje y código
                CustomException customEx = new CustomException("007", $"Este producto pertenece a La categoría no disponible: {ex.Message}.");

                // Llamar al método de manejo de excepciones
                await HandleExceptionAsync(context, customEx);
            }

            catch (Exception ex)
            {
                _logger.LogError($"ERROR 00-MDFK : {ex.Message} {ex.StackTrace}");
                CustomException exx = new CustomException("00-MDFK", $"Error no controlado {ex.Message}         {ex.StackTrace}");
                await HandleExceptionAsync(context, exx);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, CustomException ex)
        {
            var userId = context.User.Identity.IsAuthenticated
                ? context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value
                : "Usuario no autenticado";

            _logger.LogError($"Error {ex.CodigoError} para el usuario {userId}: {ex.MensajeUsuario}");

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex.CodigoError switch
            {
                "006" => (int)HttpStatusCode.Conflict, // DatoDuplicadoException - 409 Conflict
                "007" => (int)HttpStatusCode.Conflict, // Un prod con cat no disponible no puede estar disponible - 409 Conflict

                "404" => (int)HttpStatusCode.NotFound, // ResourceNotFoundException - 404 Not Found

                _ => (int)HttpStatusCode.InternalServerError // Otros errores - 500 Internal Server Error
            };

            return context.Response.WriteAsJsonAsync(new GenericResponse
            {
                Codigo = ex.CodigoError,
                Mensaje = ex.MensajeUsuario
            });
        }

    }
}

