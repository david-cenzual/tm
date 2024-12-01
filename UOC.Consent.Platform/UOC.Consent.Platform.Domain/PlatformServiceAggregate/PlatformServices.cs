using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;

namespace UOC.Consent.Platform.Domain.PlatformServiceAggregate;

public class PlatformServices
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    public                  Guid    EnterpriseReference { get; set; }
    [MaxLength(120)] public string? Name                { get; set; } // Maybe required
    public                  bool    IsRemuneration      { get; set; }
    public                  bool    IsDistance          { get; set; }
    public                  bool    IsElectronic        { get; set; }
    public                  bool    IsRequested         { get; set; }

    [ForeignKey(nameof(EnterpriseReference))]
    public virtual required Enterprises Enterprise { get; set; }
}