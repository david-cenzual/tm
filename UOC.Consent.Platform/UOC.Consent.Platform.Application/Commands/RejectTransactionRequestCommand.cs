using MediatR;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record RejectTransactionRequestCommand(int Id) : IRequest<Result>;

public class RejectTransactionRequestCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<RejectTransactionRequestCommand, Result>
{
    public async Task<Result> Handle(RejectTransactionRequestCommand request, CancellationToken ct)
    {
        // A user or enterprise indicates that he accepts the new proposal of Consents
        // We have to get the TransactionRequest
        // Create the Transaction that contains the TransactionRequest data
        // This transaction will be linked to the Consents that the TransactionRequest asked
        // This transaction must use the last transaction Hash and calculate the new Hash
        return Result.Success();
    }
}