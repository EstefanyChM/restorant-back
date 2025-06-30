
using BDRiccosModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Validaciones;
using RequestResponseModel;

namespace DTO
{
	public class VentaOnlineCrearDTO
	{
    public virtual int IdComprobante { get; set; } 

    public virtual int IdCliente { get; set; } 

    public virtual PedidoRequest MiPedidoRequest { get; set; } = null!;

    }

}
