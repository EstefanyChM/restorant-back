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
	public class BoletaPdf : IDocument
    {
        private readonly Venta _venta;
    
        public BoletaPdf(Venta  venta)
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
                page.Footer().Element(ComposeFooter);
            });
        }

        // Composición de la cabecera
        void ComposeHeader(IContainer container)
        {
            container.PaddingBottom(10).Row(row =>
            {
                // Información de la empresa
                row.RelativeItem()
                   .Column(column =>
                   {
                       column.Item().Text("POLLERIA RICCOS").Bold().FontSize(16);
                       column.Item().Text("DE: ZANABRIA CASAS ESTEFANI DEISI").Bold();
                       column.Item().Text("R.U.C.: 10750198410");
                       column.Item().Text("PRO. SAN CARLOS 127, Huancayo, Huancayo - JUNÍN");
                       column.Item().Text("Central telefónica: 952891891");
                       column.Item().Text("Email: polleriariccos@sisili.com");
                   });
        
                // Información de la boleta
                row.ConstantItem(150)
                   .Column(column =>
                   {
                       column.Item().AlignRight().Text("BOLETA ELECTRÓNICA").Bold().FontSize(14);
                       column.Item().AlignRight().Text($"B001-{_venta.Id:D8}").Bold(); // Número de la boleta
                   });
            });
        }

        // Composición del contenido
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                // Información del cliente y método de pago
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text($"Cliente:       {_venta.IdClienteNavigation.IdTablaPersonaNaturalNavigation.Nombre}     {_venta.IdClienteNavigation.IdTablaPersonaNaturalNavigation.Apellidos}");
                        column.Item().Text($"Método de Pago: {_venta.IdMetodoPagoNavigation.Nombre}");
                    });
                });

                // Información del servicio
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        string servicioTexto = _venta.IdPedidoNavigation.IdServicio == 2 
                   ? "Delivery" 
                   : _venta.IdPedidoNavigation.IdServicio == 3 
                       ? "Recojo en Tienda" 
                       : "En Tienda";


                        column.Item().Text($"SERVICIO: {servicioTexto}").Bold().FontSize(16);;


                        // Accede al primer elemento de Deliveries, si existe
               var delivery = _venta.IdPedidoNavigation.Deliveries.FirstOrDefault();
               if (delivery != null)
               {
                   column.Item().AlignRight().Text($"Dirección: {delivery.Address}").Bold().FontSize(14);
                   column.Item().AlignRight().Text($"Referencia: {delivery.Reference}").Bold().FontSize(12);

               }

               // Accede al primer elemento de Pickups, si existe
               var pickup = _venta.IdPedidoNavigation.Pickups.FirstOrDefault();
               if (pickup != null)
               {
                   column.Item().AlignRight().Text($"Hora de Entrega: {pickup.PickupTime.ToString("g")}").Bold().FontSize(14); // Formatea PickupTime como necesario
               }



                    });
                });
        
                // Separador entre información del cliente y detalle de productos
                column.Item().PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Medium);
        
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
                    columns.RelativeColumn(1); // Precio Unitario
                    columns.RelativeColumn(1); // Subtotal
                });
        
                // Encabezado de la tabla
                table.Header(header =>
                {
                    header.Cell().Text("CANT.").Bold();
                    header.Cell().Text("DESCRIPCIÓN").Bold();
                    header.Cell().Text("P. UNIT.").Bold();
                    header.Cell().Text("SUBTOTAL").Bold();
                });
        
                // Filas con los detalles de cada producto
                foreach (var detalle in _venta.IdPedidoNavigation.DetallePedidos)
                {
                    table.Cell().Text(detalle.Cantidad.ToString());
                    table.Cell().Text(detalle.IdProductoNavigation.Nombre);
                    table.Cell().Text(detalle.PrecioUnitario.ToString("F2"));
                    table.Cell().Text(detalle.SubTotal.ToString("F2"));
                }
        
                // Total de la venta
                table.Cell().ColumnSpan(3).AlignRight().Text("TOTAL: S/").Bold();
                table.Cell().Text(_venta.IdPedidoNavigation.Total.ToString("F2")).Bold();
            });
        }

        // Composición del pie de página
        void ComposeFooter(IContainer container)
        {
            container.PaddingTop(10).Column(column =>
            {
                // Total en letras
                column.Item().Text($"Son: {ConvertirNumeroALetras(_venta.IdPedidoNavigation.Total)}");
        
                // Representación de la boleta
                column.Item().Text("REPRESENTACIÓN IMPRESA DE LA BOLETA ELECTRÓNICA").FontSize(8).Italic();
        
                // Espacio para el código QR
                //column.Item().Image("/path/to/qr-code-image.png");
            });
        }

        public static string ConvertirNumeroALetras(decimal numero)
        {
            if (numero == 0) return "cero soles con 00 céntimos";
        
            long parteEntera = (long)numero;
            int parteDecimal = (int)((numero - parteEntera) * 100);  // Extrae dos decimales
        
            string letrasParteEntera = ConvertirParteEnteraALetras(parteEntera) + " soles";
            string letrasParteDecimal = parteDecimal > 0 ? $"con {parteDecimal:00} céntimos" : "con 00 céntimos";
        
            return $"{letrasParteEntera} {letrasParteDecimal}".Trim();
        }
        
        private static string ConvertirParteEnteraALetras(long numero)
        {
            if (numero == 0) return "cero";
        
            string[] unidades = { "", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
            string[] decenas = { "", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
            string[] especiales = { "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve" };
            string[] centenas = { "", "cien", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos",        "ochocientos", "novecientos" };
        
            if (numero < 10)
                return unidades[numero];
            if (numero < 20)
                return especiales[numero - 11];
            if (numero < 100)
            {
                int unidad = (int)(numero % 10);
                int decena = (int)(numero / 10);
                if (unidad == 0)
                    return decenas[decena];
                else
                    return decenas[decena] + " y " + unidades[unidad];
            }
            if (numero < 1000)
            {
                int centena = (int)(numero / 100);
                long resto = numero % 100;
                if (resto == 0)
                    return centenas[centena];
                if (centena == 1 && resto > 0)
                    return "ciento " + ConvertirParteEnteraALetras(resto);
                else
                    return centenas[centena] + " " + ConvertirParteEnteraALetras(resto);
            }
            if (numero < 1000000) // Miles
            {
                long miles = numero / 1000;
                long resto = numero % 1000;
                string milesTexto = miles == 1 ? "mil" : ConvertirParteEnteraALetras(miles) + " mil";
                if (resto == 0)
                    return milesTexto;
                return milesTexto + " " + ConvertirParteEnteraALetras(resto);
            }
            if (numero < 1000000000000) // Millones
            {
                long millones = numero / 1000000;
                long resto = numero % 1000000;
                string millonesTexto = millones == 1 ? "un millón" : ConvertirParteEnteraALetras(millones) + " millones";
                if (resto == 0)
                    return millonesTexto;
                return millonesTexto + " " + ConvertirParteEnteraALetras(resto);
            }
        
            return "Número demasiado grande";
        }

    }
}
