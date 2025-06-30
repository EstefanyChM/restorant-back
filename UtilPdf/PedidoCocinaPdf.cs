using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDRiccosModel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RequestResponseModel;

namespace UtilPdf
{
    public class PedidoCocinaPdf : IDocument
    {
        private readonly Venta _venta;

        public PedidoCocinaPdf(Venta venta)
        {
            _venta = venta;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                // Composición de la cabecera, contenido y pie de página
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

            });
        }

        // Composición de la cabecera
        void ComposeHeader(IContainer container)
{
    container.PaddingBottom(10).Row(row =>
    {
        // Primera columna
        row.RelativeItem()
           .Column(column =>
           {
               // Determina el texto según el IdServicio
               string servicioTexto = _venta.IdPedidoNavigation.IdServicio == 2 
                   ? "Delivery" 
                   : _venta.IdPedidoNavigation.IdServicio == 3 
                       ? "Recojo en Tienda" 
                       : "Otro Servicio";

               column.Item().Text(servicioTexto).Bold().FontSize(16); // SERVICIO
           });

        // Segunda columna
        row.ConstantItem(150)
           .Column(column =>
           {
               // Accede al primer elemento de Deliveries, si existe
               var delivery = _venta.IdPedidoNavigation.Deliveries.FirstOrDefault();
               if (delivery != null)
               {
                   column.Item().AlignRight().Text(delivery.Address).Bold().FontSize(14);
               }

               // Accede al primer elemento de Pickups, si existe
               var pickup = _venta.IdPedidoNavigation.Pickups.FirstOrDefault();
               if (pickup != null)
               {
                   column.Item().AlignRight().Text(pickup.PickupTime.ToString("g")).Bold().FontSize(14); // Formatea PickupTime como necesario
               }
           });
    });
}



        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                // Tabla de productos
                column.Item().PaddingVertical(10).Element(ComposeTable);
            });
        }



        // Composición de la tabla con detalles de los productos
        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // Definición de columnas
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1); // Cantidad
                    columns.RelativeColumn(3); // Descripción
                });

                // Encabezado de la tabla
                table.Header(header =>
                {
                    header.Cell().Text("CANT.").Bold();
                    header.Cell().Text("DESCRIPCIÓN").Bold();
                });

                // Filas con los detalles de cada producto
                foreach (var detalle in _venta.IdPedidoNavigation.DetallePedidos)
                {
                    table.Cell().Text(detalle.Cantidad.ToString());
                    table.Cell().Text(detalle.IdProductoNavigation.Nombre);
                }
            });
        }

    }
}
