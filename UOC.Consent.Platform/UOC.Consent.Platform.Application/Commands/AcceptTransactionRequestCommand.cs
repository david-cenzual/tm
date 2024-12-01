using MediatR;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record AcceptTransactionRequestCommand(int Id, Guid ResponderReference) : IRequest<Result>;

public class AcceptTransactionRequestCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AcceptTransactionRequestCommand, Result>
{
    public async Task<Result> Handle(AcceptTransactionRequestCommand request, CancellationToken ct)
    {
        var transactionRequest = await dbContext.GetByPk<TransactionRequests>(request.Id);
        if (transactionRequest.IsFailure)
        {
            return Result.Failure(transactionRequest.Error);
        }

        var lastLedgerTransaction = dbContext.Transactions.LastOrDefault();
        if (lastLedgerTransaction is null)
        {
            return Result.Failure(DomainError.UnexpectedError("No Transactions on Ledger"));
        }

        return transactionRequest
            .Bind<TransactionRequests, Transactions>(transactionInstance =>
            {
                var requester = transactionInstance.DataSubjectReference == request.ResponderReference
                    ? transactionInstance.DataSubjectReference
                    : transactionInstance.EnterpriseReference;

                if (requester is null)
                {
                    return Result.Failure<Transactions>(DomainError.ValidationError("Requester invalid"));
                }

                return new Transactions
                {
                    Consents             = transactionInstance.ConsentsIdentifiers.CalculateConsents().ToList(),
                    PreviousHash         = lastLedgerTransaction.Hash,
                    Hash                 = lastLedgerTransaction.CalculateNextHash(),
                    EnterpriseReference  = transactionInstance.EnterpriseReference,
                    DataSubjectReference = transactionInstance.DataSubjectReference,
                    Requester            = requester.Value,
                    Responder            = request.ResponderReference,
                    TimeStamp = DateTime.UtcNow,
                    Data = transactionInstance.Justification,
                    ConsentsIdentifiers = transactionInstance.ConsentsIdentifiers,
                    ServiceReference = transactionInstance.ServiceReference
                };
            });
    }
}