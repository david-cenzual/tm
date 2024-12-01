using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;

namespace UOC.Consent.Platform.Infrastructure.Services;

public class LedgerService(ApplicationDbContext appDbContext)
{
    public int  LedgerInternalId = int.MinValue;
    public Guid LedgerReference  = Guid.Empty;

    public void InitializeLedger()
    {
        var existingLedgers = GetLedgersWhere(_ => true).ToList();
        var selectedLedger  = ObtainGenesisLedger(existingLedgers);
        SetupInternalLedgersService(selectedLedger);
        appDbContext.SaveChanges();
    }

    private Ledgers ObtainGenesisLedger(IList<Ledgers> ledgers) =>
        ledgers switch
        {
            { Count: 1 } => appDbContext.Ledgers.Entry(ledgers.Single()).Entity,
            { Count: 0 } => appDbContext.Ledgers.Add(new Ledgers()).Entity,
            _            => throw new ApplicationException("Unexpected state, ledgers should never be more than one"),
        };

    private IEnumerable<Ledgers> GetLedgersWhere(Func<Ledgers, bool> predicate) => appDbContext.Ledgers.Where(predicate);

    private void SetupInternalLedgersService(Ledgers ledger)
    {
        LedgerInternalId = ledger.Id;
        LedgerReference  = ledger.Reference;
    }
}