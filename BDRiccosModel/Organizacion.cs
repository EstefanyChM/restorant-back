using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("organizacion", Schema = "organizacion")]
public partial class Organizacion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(300)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    [Column("ruc")]
    [StringLength(11)]
    [Unicode(false)]
    public string? Ruc { get; set; }

    [Column("webpage")]
    [Unicode(false)]
    public string? Webpage { get; set; }
}
