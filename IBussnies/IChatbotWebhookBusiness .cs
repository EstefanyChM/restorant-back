using BDRiccosModel;
using RequestResponseModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using Azure.Core;

namespace IBussnies
{
	public interface IChatbotWebhookBusiness
	{
		Task<object> ProductoPrecio(string producto);
		Task<object> Producto(string producto);
		Task<object> HorariosAtencion(string fecha);
		Task<object> ProductosRango(double? v1, double? v2, string? limite);
		/******************/



		Task<object> NuestrosServicios();
		Task<string> RealizarVenta();

		Task<object> MetodosPago();
		Task<string> AgregarCarrito(string producto, int cantidad);
		Task<object> Promocion();

		Task<object> Empresa();
		Task<object> ProductosPorCategoria(string categoria);
		Task<object> Categorias();
		Task<string> ManejarIntencionGenerica();
	}
}
