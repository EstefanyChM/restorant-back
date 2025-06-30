using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("Mesa", Schema = "mesa")]
public partial class Mesa
{
    [Key]
    public int Id { get; set; }

    public bool Estado { get; set; }

    public int Numero { get; set; }

    public bool Disponible { get; set; }

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<EnTienda> EnTienda { get; set; } = new List<EnTienda>();
}
