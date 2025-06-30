using AutoMapper;
using BDRiccosModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using IBussnies;
using IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Repository;
using RequestResponseModel;
using System.Collections.Generic;
using UtilPdf;
using QuestPDF.Fluent;
using System.IO;
using UtilPdf;

namespace Bussnies
{
    public class VentaBussnies : IVentaBussnies
    {
        /*INYECCIÓN DE DEPENDECIAS*/
        #region DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE
        private readonly IVentaRepository _VentaRepository;
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;

        public VentaBussnies(IMapper mapper,
            IPedidoRepository pedidoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _VentaRepository = new VentaRepository();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion DECLARACIÓN DE VARIABLES Y CONSTRUCTOR / DISPOSE

        #region START CRUD METHODS
        public async Task<List<VentaResponse>> GetAll()
        {
            List<Venta> Ventas = await _VentaRepository.GetAll();
            List<VentaResponse> lstResponse = _mapper.Map<List<VentaResponse>>(Ventas);
            return lstResponse;
        }

        public async Task<List<VentaResponse>> GetDeliveryPendiente(short idService)
        {
            List<Venta> Ventas = await _VentaRepository.GetListaVentaOnlinePagada(idService);
            List<VentaResponse> lstResponse = _mapper.Map<List<VentaResponse>>(Ventas);
            return lstResponse;
        }

        public async Task<VentaResponse> GetById(int id)
        {
            Venta Venta = await _VentaRepository.GetById(id);
            VentaResponse resul = _mapper.Map<VentaResponse>(Venta);
            return resul;
        }

        public async Task<VentaResponse> Create(VentaRequest entity)
        {
            Pedido pedido = await _pedidoRepository.GetById(entity.IdPedido);
            pedido.VentaFinalizada = true;
            await _pedidoRepository.Update(pedido);

            TimeZoneInfo limaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");

            // Convierte la fecha actual UTC a la hora local de Lima
            entity.FechaVenta = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, limaTimeZone);
            Venta venta = _mapper.Map<Venta>(entity);

            venta = await _VentaRepository.Create(venta);
            VentaResponse result = _mapper.Map<VentaResponse>(venta);




            return result;
        }

        public async Task<List<VentaResponse>> CreateMultiple(List<VentaRequest> lista)
        {
            List<Venta> Ventas = _mapper.Map<List<Venta>>(lista);
            Ventas = await _VentaRepository.CreateMultiple(Ventas);
            List<VentaResponse> result = _mapper.Map<List<VentaResponse>>(Ventas);
            return result;
        }

        public async Task<VentaResponse> Update(VentaRequest entity)
        {
            Venta Venta = _mapper.Map<Venta>(entity);
            Venta = await _VentaRepository.Update(Venta);
            VentaResponse result = _mapper.Map<VentaResponse>(Venta);
            return result;
        }

        public async Task<List<VentaResponse>> UpdateMultiple(List<VentaRequest> lista)
        {
            List<Venta> Ventas = _mapper.Map<List<Venta>>(lista);
            Ventas = await _VentaRepository.UpdateMultiple(Ventas);
            List<VentaResponse> result = _mapper.Map<List<VentaResponse>>(Ventas);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int cantidad = await _VentaRepository.Delete(id);
            return cantidad;
        }

        public async Task<int> DeleteMultipleItems(List<VentaRequest> lista)
        {
            List<Venta> Ventas = _mapper.Map<List<Venta>>(lista);
            int cantidad = await _VentaRepository.DeleteMultipleItems(Ventas);
            return cantidad;
        }

        public async Task<GenericFilterResponse<VentaResponse>> GetByFilter(GenericFilterRequest request)
        {

            GenericFilterResponse<VentaResponse> result = _mapper.Map<GenericFilterResponse<VentaResponse>>(_VentaRepository.GetByFilter(request));

            return result;
        }

        public async Task<VentaResponse> GetVentaDetalladaById(int id)
        {
            VistaVenta vistaVenta = await _VentaRepository.GetVistaVentaById(id);
            List<VistaDetallePedido> vistaDetallePedidoo = await _VentaRepository.GetListaVistaDetallePedido(id);

            VentaResponse ventaResponse = _mapper.Map<VentaResponse>(vistaVenta);
            // ventaResponse.vistaDetallePedido = vistaDetallePedidoo;

            return ventaResponse;
        }

        public async Task<byte[]> GenerarBoletaPDF(int idVenta)
        {
            Venta venta = await _VentaRepository.GetVentaBoletaFactura(idVenta);

            if (venta.IdClienteNavigation.IdTablaPersonaNatural != null)
            {
                // Crear el documento PDF con la    información de  la venta
                var document = new BoletaPdf(venta);
                using var memoryStream = new MemoryStream();
                document.GeneratePdf(memoryStream);

                // Devolver el contenido del PDF como un    array  de bytes
                return memoryStream.ToArray();
            }
            else
            {
                var document = new FacturaPdf(venta);
                using var memoryStream = new MemoryStream();
                document.GeneratePdf(memoryStream);

                // Devolver el contenido del PDF como un    array  de bytes
                return memoryStream.ToArray();
            }
        }



        public async Task<byte[]> GenerarPDFDetallesCocina(int idVenta)
        {
            Venta venta = await _VentaRepository.GetDetallesCocina(idVenta);

            var document = new PedidoCocinaPdf(venta);
            using var memoryStream = new MemoryStream();
            document.GeneratePdf(memoryStream);

            // Devolver el contenido del PDF como un    array  de bytes
            return memoryStream.ToArray();

        }



        public Task<int> LogicDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VentaResponse> Patch(int id, JsonPatchDocument<VentaRequest> patchDocument)
        {
            throw new NotImplementedException();
        }

        #endregion END CRUD METHODS
    }
}
