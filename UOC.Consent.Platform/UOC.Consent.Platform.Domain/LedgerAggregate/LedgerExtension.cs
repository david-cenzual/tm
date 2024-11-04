namespace UOC.Consent.Platform.Domain.LedgerAggregate;

public static class LedgerExtensions
{
    public static IEnumerable<Transaction.Transaction> InitializeChain(Guid userRef, Guid enterRef, Guid serviceRef)
        => new List<Transaction.Transaction>().AddGenesisBlock(userRef, enterRef, serviceRef);

    public static IEnumerable<Transaction.Transaction> AddGenesisBlock(
        this IEnumerable<Transaction.Transaction> transactions,
        Guid userReference,
        Guid enterpriseReference,
        Guid serviceReference) => transactions
        .Append(new Transaction.Transaction(
            0,
            $"{enterpriseReference}/${serviceReference}/{userReference}",
            "User - Enterprise Ledger",
            Guid.NewGuid(),
            userReference,
            enterpriseReference,
            serviceReference));
}