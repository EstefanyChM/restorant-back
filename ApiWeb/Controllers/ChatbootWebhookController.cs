using IBussnies;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ChatbotWebhookController : ControllerBase
{
	private readonly IChatbotWebhookBusiness _chatbotWebhookBusiness;

	public ChatbotWebhookController(IChatbotWebhookBusiness chatbotWebhookBusiness)
	{
		_chatbotWebhookBusiness = chatbotWebhookBusiness;
	}

	[HttpPost]
	public async Task<IActionResult> HandleWebhook()
	{
		var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
		Console.WriteLine($"Request JSON: {requestBody}");

		var requestJson = JsonDocument.Parse(requestBody);
		var intent = requestJson.RootElement
			.GetProperty("queryResult")
			.GetProperty("intent")
			.GetProperty("displayName")
			.GetString();

		var parameters = requestJson.RootElement
			.GetProperty("queryResult")
			.GetProperty("parameters");

		string? producto = parameters.TryGetProperty("producto", out var productoProp) ? productoProp.GetString() : null;
		string? categoria = parameters.TryGetProperty("categoria", out var categoriaProp) ? categoriaProp.GetString() : null;

		double? valor1 = parameters.TryGetProperty("number", out var valor1Prop) ? valor1Prop.GetDouble() : (double?)null;

		double? valor2 = parameters.TryGetProperty("number1", out var valor2Prop) &&
				 valor2Prop.ValueKind == JsonValueKind.Number
				 ? valor2Prop.GetDouble()
				 : (double?)null;


		string? rangoAbierto = parameters.TryGetProperty("limite-abierto", out var rangoAbiertoProp) ? rangoAbiertoProp.GetString() : (string?)null;


		string? fecha = parameters.TryGetProperty("date-time", out var fechaProp) ? fechaProp.GetString() : (string?)null;
		//int? cantidadDelProducto = parameters.TryGetProperty("number", out var cantidadDelProducto1) ? cantidadDelProducto1.GetInt32() : (int?)null;


		var response = intent switch
		{
			"Consultar precio de un producto" => await _chatbotWebhookBusiness.ProductoPrecio(producto),
			"Consultar por un producto" => await _chatbotWebhookBusiness.Producto(producto),
			"Horario de atencion" => await _chatbotWebhookBusiness.HorariosAtencion(fecha),
			"Productos dentro de un rango" => await _chatbotWebhookBusiness.ProductosRango(valor1, valor2, rangoAbierto),
			"Consulta de nuestros Servicios" => await _chatbotWebhookBusiness.NuestrosServicios(),
			"Intension de Compra" => await _chatbotWebhookBusiness.RealizarVenta(),
			"Consulta Metodos de Pago" => await _chatbotWebhookBusiness.MetodosPago(),
			"Agregar Producto Al Carrito" => await _chatbotWebhookBusiness.AgregarCarrito(producto, 0),
			"Consulta de Promociones" => await _chatbotWebhookBusiness.Promocion(),
			"Consulta de la Empresa" => await _chatbotWebhookBusiness.Empresa(),
			"Ver Los Productos por Categoria" => await _chatbotWebhookBusiness.ProductosPorCategoria(categoria),
			"Consultar categorias" => await _chatbotWebhookBusiness.Categorias(),

			_ => await _chatbotWebhookBusiness.ManejarIntencionGenerica()
		};

		return Ok(response);
	}
}
