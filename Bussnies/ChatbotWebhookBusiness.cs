using AutoMapper;
using BDRiccosModel;
using IBussnies;
using IRepository;
using Repository;
using RequestResponseModel;
using DTO;
using ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.JsonPatch;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http.HttpResults;
using Firebase.Auth;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using IServices;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Globalization;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using EllipticCurve.Utils;
using Microsoft.IdentityModel.Tokens;

namespace Bussnies
{
	public class ChatbotWebhookBusiness : IChatbotWebhookBusiness
	{
		private readonly IProductoRepository _productoRepository;
		private readonly IHorarioAtencionRepository _horarioAtencionRepository;
		private readonly IServiciosRepository _serviciosRepository;
		private readonly ICategoriaRepository _categoriaRepository;
		private readonly IPromocionRepository _promocionRepository;
		private readonly IMensajeContactoRepository _mensajeContactoRepository;
		private readonly IEmpresaRepository _empresaRepository;
		private readonly IPedidoRepository _pedidoRepository;
		private readonly IMetodoPagoRepository _metodoPagoRepository;

		public ChatbotWebhookBusiness(
			IProductoRepository productoRepository,
			IHorarioAtencionRepository horarioAtencionRepository,
			IServiciosRepository serviciosRepository,
			ICategoriaRepository categoriaRepository,
			IPromocionRepository promocionRepository,
			IMensajeContactoRepository mensajeContactoRepository,
			IEmpresaRepository empresaRepository,
			IPedidoRepository pedidoRepository,
			IMetodoPagoRepository metodoPagoRepository)
		{
			_productoRepository = productoRepository;
			_horarioAtencionRepository = horarioAtencionRepository;
			_serviciosRepository = serviciosRepository;
			_categoriaRepository = categoriaRepository;
			_promocionRepository = promocionRepository;
			_mensajeContactoRepository = mensajeContactoRepository;
			_empresaRepository = empresaRepository;
			_pedidoRepository = pedidoRepository;
			_metodoPagoRepository = metodoPagoRepository;
		}


		public string ConvertirArgumentodateTimeADiaDeLaSemana(string fecha)
		{
			DateTime date = DateTime.Parse(fecha);
			string dayOfWeek = date.ToString("dddd", new CultureInfo("es-ES"));
			return dayOfWeek;
		}

		public async Task<object> ProductoPrecio(string producto)
		{
			List<Producto> prod = await _productoRepository.ObtenerProductosParaChatBot(producto);

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = prod.Select(cat => new List<object>
			{
				new
				{
					type = "image",
					rawUrl = cat.UrlImagen, // Asegúrate de que la entidad Categoria tenga esta propiedad
			        accessibilityText = cat.Nombre
				},
				new
				{
					type = "info",
					title = cat.Nombre,
					subtitle = $"S./ {cat.Precio} - x unidad",

					anchor = new { href = cat.UrlImagen } // Asegúrate de que la entidad Categoria tenga esta propiedad
			    },

			}).ToList();

			// Manejo de lista vacía
			if (!prod.Any())
			{
				return new
				{
					fulfillmentMessages = new[]
					{
						new
						{
							text = new
							{
								text = new[] { "No tenemos ese producto" }
							}
						}
					}
				};
			}

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = richContent
						}
					}
				}
			};
		}


		public async Task<object> Producto(string producto)
		{
			List<Producto> prod = await _productoRepository.ObtenerProductosParaChatBot(producto);

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = prod.Select(cat => new List<object>
			{
				new
				{
					type = "image",
					rawUrl = cat.UrlImagen, // Asegúrate de que la entidad Categoria tenga esta propiedad
			        accessibilityText = cat.Nombre
				},
				new
				{
					type = "info",
					title = cat.Nombre,
					//subtitle = cat.Descripcion,
					anchor = new { href = cat.UrlImagen } // Asegúrate de que la entidad Categoria tenga esta propiedad
			    },

			}).ToList();

			// Manejo de lista vacía
			if (!prod.Any())
			{
				return new
				{
					fulfillmentMessages = new[]
					{
						new
						{
							text = new
							{
								text = new[] { "No tenemos ese producto" }
							}
						}
					}
				};
			}

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = richContent
						}
					}
				}
			};
		}

		public async Task<object> HorariosAtencion(string? fecha)
		{
			List<HorariosRegulares> horariosRegulares = string.IsNullOrEmpty(fecha) ?
				await _horarioAtencionRepository.GetAll() :
				await _horarioAtencionRepository.GetAllQueryable()
					.Where(cat => cat.DiaSemana == ConvertirArgumentodateTimeADiaDeLaSemana(fecha))
				.ToListAsync();



			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = new List<object>();

			foreach (var mp in horariosRegulares)
			{
				richContent.Add(new
				{
					type = "list",
					title = mp.DiaSemana ?? "Día",
					subtitle = $"{mp.HoraApertura} - {mp.HoraCierre}",
					eventData = new
					{
						name = "",
						languageCode = "",
						parameters = new { }
					}
				});

				// Agregar un divisor entre elementos (opcional)
				richContent.Add(new { type = "divider" });
			}



			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = new List<List<object>> { richContent } // Se envuelve en una lista de listas
		                }
					}
				}
			};
		}


		public async Task<object> ProductosRango(double? v1, double? v2, string? limiteAbierto)
		{
			List<Producto> productos = await _productoRepository.GetAll();

			if (!string.IsNullOrWhiteSpace(limiteAbierto)) // Rango abierto
			{
				productos = limiteAbierto == "mayor" ?
							productos.Where(x => (double?)x.Precio > v1).ToList() :
							productos.Where(x => (double?)x.Precio < v1).ToList();
			}
			else // Rango cerrado
			{
				if (v1 > v2) { (v1, v2) = (v2, v1); }  // Intercambia los valores
				productos = productos.Where(x => (double?)x.Precio < v2 && (double?)x.Precio > v1).ToList();
			}

			// Manejo de lista vacía
			if (!productos.Any())
			{
				return new
				{
					fulfillmentMessages = new[]
					{
						new
						{
							text = new
							{
								text = new[] { "No se encontraron productos en el rango de precios especificado." }
							}
						}
					}
				};
			}

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = new List<object>();

			foreach (var mp in productos)
			{
				richContent.Add(new
				{
					type = "list",
					title = mp.Nombre,
					subtitle = $"S./ {mp.Precio} - x unidad",
					eventData = new
					{
						name = "",
						languageCode = "",
						parameters = new { }
					}
				});
			}

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = new List<List<object>> { richContent } // Se envuelve en una lista de listas
		                }
					}
				}
			};
		}

		public async Task<object> NuestrosServicios()
		{
			// Obtener la lista de métodos de pago activos
			List<Service> servicios = await _serviciosRepository.GetAllQueryable()
				.ToListAsync();

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = new List<object>();

			foreach (var mp in servicios)
			{
				richContent.Add(new
				{
					type = "list",
					title = mp.Name ?? "Servicio",
					subtitle = mp.Description ?? "Descripción no disponible",
					eventData = new
					{
						name = "",
						languageCode = "",
						parameters = new { }
					}
				});

				// Agregar un divisor entre elementos (opcional)
				richContent.Add(new { type = "divider" });
			}

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = new List<List<object>> { richContent } // Se envuelve en una lista de listas
		                }
					}
				}
			};
		}

		public async Task<string> RealizarVenta()
		{
			// Obtener la dirección de la tienda
			string local = (await _empresaRepository.GetById(1)).Direccion;
			throw new NotImplementedException();

		}


		public async Task<object> MetodosPago()
		{
			// Obtener la lista de métodos de pago activos
			/*List<MetodoPago> metodoPagos = await new CRUDRepository<MetodoPago>().GetAllQueryable()
				.Where(mp => mp.Estado == true)
				.ToListAsync();*/

			List<MetodoPago> metodoPagos = await _metodoPagoRepository.GetAllQueryable()
				.Where(mp => mp.Estado == true)
				.ToListAsync();

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = new List<object>();

			foreach (var mp in metodoPagos)
			{
				richContent.Add(new
				{
					type = "list",
					title = mp.Nombre ?? "Método de Pago",
					subtitle = mp.Descripcion ?? "Descripción no disponible",
					eventData = new
					{
						name = "",
						languageCode = "",
						parameters = new { }
					}
				});

				// Agregar un divisor entre elementos (opcional)
				richContent.Add(new { type = "divider" });
			}

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = new List<List<object>> { richContent } // Se envuelve en una lista de listas
		                }
					}
				}
			};
		}

		public async Task<string> AgregarCarrito(string producto, int cantidad)
		{
			// Obtener la dirección de la tienda
			string local = (await _empresaRepository.GetById(1)).Direccion;

			// Construir la respuesta
			string htmlResponse = $@"
		        <div style='font-family: Arial, sans-serif; color: #333;'>
		            <p style='font-size: 1.2em;'>¡Lo sentimos! Actualmente, este servicio	no	está disponible directamente en el chatbot. </p>
		            <p style='font-size: 1em;'>Sin embargo, puedes realizar tu compra en	línea	en el siguiente enlace:</p>
		            <a href='https://riccospyp-b3ee8.web.app/#/' style='font-size: 1em;		color:	#007bff; text-decoration: none;'>Compra online</a>
		            <p style='font-size: 1em; margin-top: 10px;'>O si prefieres, puedes			visitarnos en nuestra tienda ubicada en:</p>
		            <p style='font-size: 1.2em; font-weight: bold;'>{local}</p>
		        </div>
		    ";

			return htmlResponse;
		}

		public async Task<object> Promocion()
		{
			List<Promocion> promocions = await _promocionRepository.GetAllQueryable()
				.Where(cat => cat.Stock != 0)
				.ToListAsync();

			string link = $"https://riccospyp-b3ee8.web.app/#/menu/Pollos%20a%20la%20Brasa";

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = promocions.Select(cat => new List<object>
			{
				new
				{
					type = "image",
					rawUrl = cat.UrlImagen, // Asegúrate de que la entidad Categoria tenga esta propiedad
			        accessibilityText = cat.Nombre
				},
				new
				{
					type = "info",
					title = cat.Nombre,
					//subtitle = cat.Descripcion,
					anchor = new { href = cat.UrlImagen } // Asegúrate de que la entidad Categoria tenga esta propiedad
			    },
				new
				{
					type = "button",
					icon = new {
					  type = "chevron_right",
					  color= "#FF9800"
					},
					text= "Ver más",
					link= $"https://riccospyp-b3ee8.web.app/#/promotions",
				},
			}).ToList();

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = richContent
						}
					}
				}
			};
		}

		public async Task<object> Empresa()
		{
			Empresa empresa = await _empresaRepository.GetById(1);

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = new List<object>
			{
				new
				{
					type = "info",
					title = "Razón Social",
					subtitle = empresa.RazonSocial ?? "No disponible"
				},
				new
				{
					type = "info",
					title = "RUC",
					subtitle = empresa.Ruc ?? "No disponible"
				},
				new
				{
					type = "info",
					title = "Dirección",
					subtitle = empresa.Direccion ?? "No disponible"
				},
				new
				{
					type = "info",
					title = "Teléfono",
					subtitle = empresa.Telefono ?? "No disponible"
				},
				new
				{
					type = "info",
					title = "Correo",
					subtitle = empresa.Correo ?? "No disponible"
				}
			};

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = new List<List<object>> { richContent } // Se envuelve en una lista de listas
					    }
					}
				}
			};
		}

		public async Task<object> ProductosPorCategoria(string categoria)
		{
			List<Producto> productosObtenidos = await _productoRepository.GetAllQueryable()
				.Where(p => p.IdCategoriaNavigation.Nombre.ToLower().Contains(categoria.ToLower()) &&
							p.Disponibilidad == true &&
							p.Estado == true &&
							p.Stock != 0)
				.ToListAsync();

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = productosObtenidos.Select(cat => new List<object>
			{
				new
				{
					type = "image",
					rawUrl = cat.UrlImagen, // Asegúrate de que la entidad Categoria tenga esta propiedad
			        accessibilityText = cat.Nombre
				},
				new
				{
					type = "info",
					title = cat.Nombre,
					subtitle = $"S./ {cat.Precio} x unidad",
					text = cat.Descripcion
				},
			}).ToList();

			// Manejo de lista vacía
			if (!productosObtenidos.Any())
			{
				return new
				{
					fulfillmentMessages = new[]
					{
						new
						{
							text = new
							{
								text = new[] { "No se encontraron productos en el rango de precios especificado." }
							}
						}
					}
				};
			}

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = richContent
						}
					}
				}
			};
		}


		public async Task<object> Categorias()
		{
			// Obtener las categorías activas y disponibles
			List<Categoria> categorias = await _categoriaRepository.GetAllQueryable()
				.Where(cat => cat.Estado == true && cat.Disponibilidad == true)
				.ToListAsync();

			// Construir la respuesta en el formato requerido por Dialogflow
			var richContent = categorias.Select(cat => new List<object>
			{
				new
				{
					type = "image",
					rawUrl = cat.UrlImagen, // Asegúrate de que la entidad Categoria tenga esta propiedad
			        accessibilityText = cat.Nombre
				},
				new
				{
					type = "info",
					title = cat.Nombre,
					//subtitle = cat.Descripcion,
					anchor = new { href = cat.UrlImagen } // Asegúrate de que la entidad Categoria tenga esta propiedad
			    },
				new
				{
					type = "button",
					icon = new {
					  type = "chevron_right",
					  color= "#FF9800"
					},
					text= "Ver más",
					link= $"https://riccospyp-b3ee8.web.app/#/menu/{cat.Nombre}",
				},
			}).ToList();

			return new
			{
				fulfillmentMessages = new[]
				{
					new
					{
						payload = new
						{
							richContent = richContent
						}
					}
				}
			};
		}

		public async Task<string> ManejarIntencionGenerica()
		{
			return "<p>Disssculpa,no enntí</p>";
		}


	}
}
