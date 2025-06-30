using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("EmailSuscriptor", Schema = "Subscriptor")]
public partial class EmailSuscriptor
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Nombre { get; set; }

    public bool Estado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaSuscripcion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaDesuscripcion { get; set; }

    [StringLength(255)]
    public string? Preferencias { get; set; }
}
