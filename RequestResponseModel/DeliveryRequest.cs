using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RequestResponseModel;

public partial class DeliveryRequest
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Reference { get; set; }
    public int IdPedido { get; set; }

}
