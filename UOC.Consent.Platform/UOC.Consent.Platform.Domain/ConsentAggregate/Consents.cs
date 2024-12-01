using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.TransactionAggregate;

namespace UOC.Consent.Platform.Domain.ConsentAggregate;

public class Consents
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    public                  Guid                   TransactionReference  { get; set; }
    public                  bool                   IsRequired            { get; set; }
    public                  DateTime               DateGiven             { get; set; }
    [MaxLength(255)] public string?                ProcessingPurpose     { get; set; }
    [MaxLength(255)] public string?                SupportingInformation { get; set; }
    public                  ConsentStatus          Status                { get; set; }
    public                  ConsentMethod          Method                { get; set; }
    public                  PersonalDataIdentifier DataIdentifier        { get; set; }

    [ForeignKey(nameof(TransactionReference))]
    public virtual Transactions? Transaction { get; set; }

}

public static class ConsentsExtensions
{
    public static IEnumerable<Consents> CalculateConsents(this IEnumerable<PersonalDataIdentifier> identifiers)
    {
        return identifiers
            .Select(x => new Consents
            {
                Method                = ConsentMethod.Platform,
                Status                = ConsentStatus.Given,
                DataIdentifier        = x,
                DateGiven             = DateTime.UtcNow,
                IsRequired            = true,
                ProcessingPurpose     = "",
                SupportingInformation = ""
            });

        
    }

}