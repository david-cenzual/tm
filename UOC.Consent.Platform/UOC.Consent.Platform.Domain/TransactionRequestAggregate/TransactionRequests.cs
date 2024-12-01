using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;

namespace UOC.Consent.Platform.Domain.TransactionRequestAggregate;

public class TransactionRequests
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    public                  DraftDecision            DraftDecision { get; set; }
    [MaxLength(255)] public string?                  Justification { get; set; }
    public                  TransactionRequestStatus Status        { get; set; }
    public                  Guid                     Requester     { get; set; }
    public                  Guid                     Responder     { get; set; }

    public ICollection<PersonalDataIdentifier> ConsentsIdentifiers { get; set; } = [];

    public Guid? ServiceReference { get; set; }

    [ForeignKey(nameof(ServiceReference))] 
    public virtual PlatformServices? Service { get; set; }

    public Guid? EnterpriseReference { get; set; }

    [ForeignKey(nameof(EnterpriseReference))]
    public virtual Enterprises? Enterprise { get; set; }

    public Guid? DataSubjectReference { get; set; }

    [ForeignKey(nameof(DataSubjectReference))]
    public virtual DataSubjects? DataSubject { get; set; }
}