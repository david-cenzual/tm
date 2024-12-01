using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;

namespace UOC.Consent.Platform.Domain.DataSubjectAggregate;

public class DataSubjects
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    public required Guid ElectronicIdRef { get; set; }

    [ForeignKey(nameof(ElectronicIdRef))] 
    public virtual ElectronicIds? ElectronicId { get; set; }

    public virtual ICollection<TransactionRequests> TransactionRequests { get; set; } = [];
}