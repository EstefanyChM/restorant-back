using BDRiccosModel;
using RequestResponseModel;
using DTO;
using Microsoft.AspNetCore.JsonPatch;
using Azure.Core;

namespace IBussnies
{
	public interface IHorarioAtencionBusiness : ICRUDBussnies<HorarioAtencionRequest, HorarioAtencionResponse>
	{
	}
}
