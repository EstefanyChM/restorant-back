using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("PersonaJuridica", Schema = "persona")]
public partial class PersonaJuridica
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Ruc { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string RazonSocial { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? NombreComercial { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Tipo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Condicion { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Departamento { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Provincia { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Distrito { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Ubigeo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Capital { get; set; }

    public short IdPersonaTipoDocumento { get; set; }

    [InverseProperty("IdTablaPersonaJuridicaNavigation")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [ForeignKey("IdPersonaTipoDocumento")]
    [InverseProperty("PersonaJuridicas")]
    public virtual PersonaTipoDocumento IdPersonaTipoDocumentoNavigation { get; set; } = null!;
}
