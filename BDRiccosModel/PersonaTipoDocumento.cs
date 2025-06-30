using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BDRiccosModel;

[Table("PersonaTipoDocumento", Schema = "persona")]
public partial class PersonaTipoDocumento
{
    [Key]
    public short Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    [InverseProperty("IdPersonaTipoDocumentoNavigation")]
    public virtual ICollection<PersonaJuridica> PersonaJuridicas { get; set; } = new List<PersonaJuridica>();

    [InverseProperty("IdPersonaTipoDocumentoNavigation")]
    public virtual ICollection<PersonaNatural> PersonaNaturals { get; set; } = new List<PersonaNatural>();
}
