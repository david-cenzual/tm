using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Domain.LedgerAggregate;

public class Ledger
{
    public int  Id        { get; set; }
    public Guid Reference { get; private set; }

    public IEnumerable<Domain.Transaction.Transaction>               Chain                { get; set; } = [];
    public IEnumerable<TransactionRequest> Requests             { get; set; } = [];
    public Guid                                                      DataSubjectReference { get; set; }
    public Guid                                                      EnterpriseReference  { get; set; }
    public Guid                                                      ServiceReference     { get; set; }

    public static async Task<Result<Ledger>> GetLedgerById(Guid id)
    {
        // DB Context Get Ledger by primary key
        return new Ledger();
    }

    public static async Task<Result<Ledger>> FindLedger(string userId, string orgId, string serviceId)
    {
        // Db context search, list should return one element, otherwise error
        return new Ledger();
    }
    
    public static Ledger Existing(
        IList<Transaction.Transaction> chain,
        IList<TransactionRequest> requests,
        Guid userRef, Guid orgRef,
        Guid serviceRef) =>
        new()
        {
            Chain                = chain,
            Reference = Guid.NewGuid(),
            Requests             = requests,
            DataSubjectReference = userRef,
            EnterpriseReference  = orgRef,
            ServiceReference     = serviceRef,
        };
    
    public static Ledger Start(Guid userRef, Guid orgRef, Guid serviceRef) =>
        new()
        {
            Chain     = LedgerExtensions.InitializeChain(userRef, orgRef, serviceRef),
            Reference = Guid.NewGuid(),
            Requests             = [], 
            DataSubjectReference = userRef,
            EnterpriseReference  = orgRef,
            ServiceReference     = serviceRef
        };

}
