using ApiWeb.Hubs;
using AutoMapper;
using BDRiccosModel;
using Bussnies;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using IBussnies;
using IRepository;
using IServices;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using RequestResponseModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProcesamientoPagosController : ControllerBase
	{
		private readonly IProductoRepository _productoRepository;
		//private readonly IHubContext<NotificationHub> _hubContext;
		private readonly IMesaRepository _mesaRepository;
		private readonly IVentaRepository _ventaRepository;
		private readonly IPedidoBussnies _pedidoBussnies;
		private readonly IPromocionRepository _promocionRepository;
		private readonly IMapper _mapper;
		private readonly IWebSocketService _webSocketService;
		public ProcesamientoPagosController(
			IProductoRepository productoRepository,
			// IHubContext<NotificationHub> hubContext,
			IMesaRepository mesaRepository,
			IVentaRepository ventaRepository,
			IPedidoBussnies pedidoBussnies,
			IPromocionRepository promocionRepository,
			IMapper mapper,
			IWebSocketService webSocketService)
		{
			_productoRepository = productoRepository;
			// _hubContext = hubContext;
			_mesaRepository = mesaRepository;
			_ventaRepository = ventaRepository;
			_pedidoBussnies = pedidoBussnies;
			_promocionRepository = promocionRepository;
			_mapper = mapper;
			_webSocketService = webSocketService;
		}

		[HttpPost("mercadopago/procesar")]
		public async Task<IActionResult> CrearPedido([FromBody] VentaOnlineCrearDTO request)
		{

			// Configura la zona horaria específica (ejemplo: Lima, Perú)
			TimeZoneInfo limaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
			DateTime peruTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, limaTimeZone);


			request.MiPedidoRequest.HoraEntrada = peruTime.TimeOfDay;
			//request.MiPedidoRequest.DetallePedidosRequest[0].IdUsuarioSistema = null;

			//request.MiPedidoRequest.DetallePedidosRequest[1].IdUsuarioSistema = null;//1 pa dar un valor  nomas // 2 qué?



			var preferenceRequest = new PreferenceRequest
			{
				Items = new List<PreferenceItemRequest>(),
				BackUrls = new PreferenceBackUrlsRequest
				{
					Success = "https://riccospyp-b3ee8.web.app/#/success", // Cambia a la ruta que maneja la confirmación en tu frontend
					Failure = "https://riccospyp-b3ee8.web.app/#/failure", // Cambia a la ruta que maneja el fallo en tu frontend
					Pending = "https://riccospyp-b3ee8.web.app/#/pending" // Cambia a la ruta que maneja el estado pendiente en tu fron/tend

					//Success = "http://localhost:4200/#/success",
					//Failure = "http://localhost:4200/#/failure",
					//Pending = "http://localhost:4200/#/pending"
				},

				AutoReturn = "approved"
			};

			List<DetallesPromocion> detallesPedidosAAgregar = new List<DetallesPromocion>();
			List<DetallePedidoRequest> detallesPedidosAEliminar = new List<DetallePedidoRequest>();


			foreach (var dp in request.MiPedidoRequest.DetallePedidosRequest)
			{
				if (dp.Id != 0)
				{
					detallesPedidosAEliminar.Add(dp);

					var detallesPromocion = await _promocionRepository.obtenerLosDPDeLaPromo(dp.Id);
					detallesPedidosAAgregar.AddRange(detallesPromocion);

					preferenceRequest.Items.Add(new PreferenceItemRequest
					{
						Title = (await _promocionRepository.GetById(dp.Id)).Nombre,
						Quantity = dp.Cantidad,
						CurrencyId = "PEN",
						UnitPrice = dp.PrecioUnitario
					});

				}
				else
				{
					dp.EstadoPreparacion = false;

					var producto = await _productoRepository.GetById(dp.IdProducto); // Asegúrate que sea un método async

					preferenceRequest.Items.Add(new PreferenceItemRequest
					{
						Title = producto.Nombre,
						Quantity = dp.Cantidad,
						CurrencyId = "PEN",
						UnitPrice = dp.PrecioUnitario
					});
				}

			}

			decimal costoDelivery = CalcularCostoDelivery(request.MiPedidoRequest); // Implementa este método para calcular el costo real

			if (request.MiPedidoRequest.IdServicio == 2)
			{
				preferenceRequest.Items.Add(new PreferenceItemRequest
				{
					Title = "Costo de Delivery",
					Quantity = 1,
					CurrencyId = "PEN",
					UnitPrice = costoDelivery
				});
			}


			var pedidoGuardado = await _pedidoBussnies.Create(request.MiPedidoRequest);
			request.MiPedidoRequest.VentaFinalizada = true;
			request.MiPedidoRequest.Estado = true;
			request.MiPedidoRequest.Finalizado = true;


			// Suponiendo que detallesPedidosAEliminar ya contiene los elementos que deseas eliminar
			request.MiPedidoRequest.DetallePedidosRequest.RemoveAll(dp => detallesPedidosAEliminar.Contains(dp));

			List<DetallePedidoRequest> pre = _mapper.Map<List<DetallePedidoRequest>>(detallesPedidosAAgregar);

			foreach (var item in pre)
			{
				item.Id = 0;
				item.EstadoPreparacion = false;
			}

			request.MiPedidoRequest.DetallePedidosRequest.AddRange(pre);


			Venta mandarVenta = new Venta
			{


				IdCliente = request.IdCliente,
				FechaVenta = peruTime,
				Estado = true,
				//IdPedido = pedidoGuardado.Id,

				IdComprobante = request.IdComprobante,

				IdEstadoVenta = 2,
				IdMetodoPago = 7,
				IdPedidoNavigation = _mapper.Map<Pedido>(request.MiPedidoRequest),
			};

			Venta ventaGuardada = await _ventaRepository.Create(mandarVenta);
			// Guarda el pedido en tu base de datos aquí
			// Ahora que el pedido está guardado, obtenemos su ID
			preferenceRequest.ExternalReference = ventaGuardada.Id.ToString(); // Asignamos el PedidoId al campo external_reference

			var client = new PreferenceClient();
			Preference preference = await client.CreateAsync(preferenceRequest);


			return Ok(new { InitPoint = preference.InitPoint, ventaGuardada.IdPedidoNavigation.Id });



		}



		// Endpoint de webhook
		[HttpPost("mercadopago/notificacion")]
		public async Task<IActionResult> RecibirNotificacion([FromBody] JObject request)
		{
			try
			{
				string topic = request["type"]?.ToString();
				string resourceId = request["data"]?["id"]?.ToString();

				if (string.IsNullOrEmpty(topic) || string.IsNullOrEmpty(resourceId))
				{
					return BadRequest("Datos de notificación incompletos.");
				}

				if (topic == "payment")
				{
					var payment = await ObtenerDetallesPago(resourceId);
					if (payment != null)
					{
						string status = payment.Status;
						string externalReference = payment.ExternalReference; // Aquí obtienes el ExternalReference

						// Actualiza el estado del pedido en la base de datos

						Venta venta = await _ventaRepository.GetById(int.Parse(externalReference));

						venta.IdEstadoVenta = 3;
						await _ventaRepository.Update(venta);

						await _webSocketService.EmitMessageAsync("pedido-finalizado-back", "");
						return Ok();
					}
				}
				return Ok(); // Responde con 200
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error procesando notificación: {ex.Message}");
				return StatusCode(500, "Error procesando la notificación.");
			}
		}

		private async Task<Payment> ObtenerDetallesPago(string paymentId)
		{
			var paymentClient = new PaymentClient();
			try
			{
				if (long.TryParse(paymentId, out long parsedPaymentId))
				{
					return await paymentClient.GetAsync(parsedPaymentId);
				}
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error obteniendo detalles del pago: {ex.Message}");
				return null;
			}
		}

		private decimal CalcularCostoDelivery(PedidoRequest request)
		{
			// Implementa la lógica para calcular el costo de delivery
			return 5; // Ejemplo de costo fijo
		}
	}
}


