using IServices;
using RequestResponseModel;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Services
{
	public class ApisPeruServices : IApisPeruServices
	{
		private readonly IConfiguration _configuration;
		ApisPeruEmpresaResponse empresaResult;
		ApisPeruPersonaResponse personaResult;
		public ApisPeruServices(IConfiguration configuration)
		{
			empresaResult = new ApisPeruEmpresaResponse();
			personaResult = new ApisPeruPersonaResponse();
			_configuration = configuration;
		}


		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


		public ApisPeruEmpresaResponse EmpresaPorRUC(string ruc)
		{
			PersonaPorRUCTask(ruc).GetAwaiter().GetResult();
			return empresaResult;
		}

		public ApisPeruPersonaResponse PersonaPorDNI(string dni)
		{
			
			PersonaPorDNITask(dni).GetAwaiter().GetResult();
			return personaResult;
		}

		public async Task PersonaPorDNITask(string dni)
		{
			string url = "https://dniruc.apisperu.com/api/v1/dni/##DNI##?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImkyODE0NzgyQGNvbnRpbmVudGFsLmVkdS5wZSJ9.wq7D-6S5gJ5JpjMggkp9robZzwDCsVwIXpiG_OOO8tU";

			url = url.Replace("##DNI##", dni);

			using (HttpClient client = new HttpClient())
			{
				//con la seguridad de C#
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
				ServicePointManager.ServerCertificateValidationCallback = delegate
				(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{ return true; };

				//https | http

				//EN EL CASO DE QUE LA URL O SERVICIO SOLICITE UN TOKEN
				// ==> client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token jwt");

				using (HttpResponseMessage response = client.GetAsync(url).Result)
				{
					if (response.StatusCode == HttpStatusCode.OK)//200
					{
						string jsonResult = await response.Content.ReadAsStringAsync();

						#pragma warning disable CS8601 // Possible null reference assignment.
						personaResult = JsonConvert.DeserializeObject<ApisPeruPersonaResponse>(jsonResult);
					}
					else
					{
						//VAMOS A MANEJAR UN CONTROL DE ERRORES
					}
				}
			}
		}


		public async Task PersonaPorRUCTask(string ruc)
		{
			string url = $"{_configuration["ApisPeru:BaseUrl"]}/ruc/##RUC##?token={_configuration["ApisPeru:Token"]}";

			url = url.Replace("##RUC##", ruc);

			using (HttpClient client = new HttpClient())
			{
				//con la seguridad de C#
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
				ServicePointManager.ServerCertificateValidationCallback = delegate
				(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{ return true; };

				using (HttpResponseMessage response = client.GetAsync(url).Result)
				{
					if (response.StatusCode == HttpStatusCode.OK)//200
					{
						string jsonResult = await response.Content.ReadAsStringAsync();

						#pragma warning disable CS8601 // Possible null reference assignment.
						empresaResult = JsonConvert.DeserializeObject<ApisPeruEmpresaResponse>(jsonResult);
						 
					}
					else
					{
						//VAMOS A MANEJAR UN CONTROL DE ERRORES
					}
				}
			}
		}

	}
}