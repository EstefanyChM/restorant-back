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
//Woocomerce
//shopping

namespace Bussnies
{
	public class HorarioAtencionBusiness : IHorarioAtencionBusiness
	{
		#region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR
		private readonly IMapper _mapper;
		private readonly IHorarioAtencionRepository _horarioAtencionRepository;

		public HorarioAtencionBusiness(IMapper mapper, IHorarioAtencionRepository horarioAtencionRepository)
		{
			_mapper = mapper;
			_horarioAtencionRepository = horarioAtencionRepository;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		#endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

		#region START CRUD METHODS
		public async Task<List<HorarioAtencionResponse>> GetAll()
		{
			List<HorariosRegulares> horarios = await _horarioAtencionRepository.GetAll();

			List<HorarioAtencionResponse> lstResponse = horarios.Select(h => new HorarioAtencionResponse
			{
				Id = h.Id,
				DiaSemana = h.DiaSemana,
				HoraApertura = TimeSpanTo12HourFormat(h.HoraApertura),
				HoraCierre = TimeSpanTo12HourFormat(h.HoraCierre)
			}).ToList();

			/*List<HorarioAtencionResponse> lstResponse = _mapper.Map<List<HorarioAtencionResponse>>(horarios);*/

			return lstResponse;
		}

		private string TimeSpanTo12HourFormat(TimeSpan time)
		{
			return DateTime.Today.Add(time).ToString("hh:mm tt"); // Formato 12 horas con AM/PM
		}

		public async Task<HorarioAtencionResponse> Update(HorarioAtencionRequest entity)
		{

			// Convertir las horas de formato string (AM/PM) a TimeSpan
			TimeSpan horaApertura = ConvertToTimeSpan(entity.HoraApertura);
			TimeSpan horaCierre = ConvertToTimeSpan(entity.HoraCierre);

			HorariosRegulares horario = new HorariosRegulares
			{
				Id = entity.Id,
				DiaSemana = entity.DiaSemana,
				HoraApertura = horaApertura,
				HoraCierre = horaCierre,
				IdEmpresa = 1

			};

			horario = await _horarioAtencionRepository.Update(horario);

			// Mapear la entidad actualizada a la respuesta
			HorarioAtencionResponse result = _mapper.Map<HorarioAtencionResponse>(horario);

			return result;
		}

		// Método para convertir la hora en formato string (AM/PM) a TimeSpan
		private TimeSpan ConvertToTimeSpan(string hora)
		{
			DateTime dateTime = DateTime.ParseExact(hora, "hh:mm tt", CultureInfo.InvariantCulture);
			return dateTime.TimeOfDay;
		}




		public async Task<HorarioAtencionResponse> GetById(int id)
		{
			HorariosRegulares horario = await _horarioAtencionRepository.GetById(id);

			HorarioAtencionResponse result = _mapper.Map<HorarioAtencionResponse>(horario);
			result.HoraApertura = TimeSpanTo12HourFormat(horario.HoraApertura);
			result.HoraCierre = TimeSpanTo12HourFormat(horario.HoraCierre);
			return result;
		}

		public Task<HorarioAtencionResponse> Create(HorarioAtencionRequest entity)
		{
			throw new NotImplementedException();
		}

		public Task<int> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<int> LogicDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<HorarioAtencionResponse> Patch(int id, JsonPatchDocument<HorarioAtencionRequest> patchDocument)
		{
			throw new NotImplementedException();
		}

		public Task<int> DeleteMultipleItems(List<HorarioAtencionRequest> lista)
		{
			throw new NotImplementedException();
		}

		public Task<List<HorarioAtencionResponse>> CreateMultiple(List<HorarioAtencionRequest> lista)
		{
			throw new NotImplementedException();
		}

		public Task<List<HorarioAtencionResponse>> UpdateMultiple(List<HorarioAtencionRequest> lista)
		{
			throw new NotImplementedException();
		}

		public Task<GenericFilterResponse<HorarioAtencionResponse>> GetByFilter(GenericFilterRequest request)
		{
			throw new NotImplementedException();
		}


		#endregion END CRUD METHODS
	}
}
