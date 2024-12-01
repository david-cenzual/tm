using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;

namespace UOC.Consent.Platform.Domain.TransactionAggregate;

public class Transactions
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    public DateTime TimeStamp            { get; set; }
    public Guid?    ServiceReference     { get; set; }
    public Guid?    EnterpriseReference  { get; set; }
    public Guid?    DataSubjectReference { get; set; }
    public Guid     LedgerReference      { get; set; }
    public Guid     Requester            { get; set; }
    public Guid     Responder            { get; set; }

    [MaxLength(255)] public string? PreviousHash { get; set; }

    [MaxLength(255)] public string Hash { get; set; } = string.Empty;

    public ICollection<PersonalDataIdentifier> ConsentsIdentifiers { get; set; } = [];

    [MaxLength(255)] public string? Data { get; set; }

    [ForeignKey(nameof(ServiceReference))] public virtual PlatformServices? Service { get; set; }

    [ForeignKey(nameof(LedgerReference))] public virtual Ledgers? Ledger { get; set; }

    [ForeignKey(nameof(EnterpriseReference))]
    public virtual Enterprises? Enterprise { get; set; }

    [ForeignKey(nameof(DataSubjectReference))]
    public virtual DataSubjects? DataSubject { get; set; }

    public virtual ICollection<Consents> Consents { get; set; } = [];
}

public static class TransactionExtensions
{
    public static string CalculateNextHash(this Transactions transaction)
    {
        var rawData = $"{transaction.TimeStamp}{transaction.Hash}";
        return BitConverter
                           .ToString(SHA256.HashData(Encoding.UTF8.GetBytes(rawData)))
                           .Replace("-", "")
                           .ToLower();
    }

}