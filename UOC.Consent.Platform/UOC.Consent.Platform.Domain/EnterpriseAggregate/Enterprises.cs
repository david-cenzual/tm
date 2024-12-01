using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;

namespace UOC.Consent.Platform.Domain.EnterpriseAggregate;

public class Enterprises
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    [MaxLength(255)] public string?   Name                        { get; set; }
    public                  LegalForm LegalForm                   { get; set; }
    public                  bool      IsEngagedInEconomicActivity { get; set; }
    public                  double    Latitude                    { get; set; }
    public                  double    Longitude                   { get; set; }

    public                                       Guid?          ElectronicIdRef { get; set; }
    [ForeignKey(nameof(ElectronicIdRef))] public ElectronicIds? ElectronicId    { get; set; }

    public virtual ICollection<Transactions>        Transactions        { get; set; } = [];
    public virtual ICollection<TransactionRequests> TransactionRequests { get; set; } = [];
    public virtual ICollection<PlatformServices>  Services            { get; set; } = [];
}