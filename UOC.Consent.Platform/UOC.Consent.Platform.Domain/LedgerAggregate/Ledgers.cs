using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Domain.LedgerAggregate;

public class Ledgers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    public virtual IEnumerable<Transactions> Chain { get; set; } = [];
}