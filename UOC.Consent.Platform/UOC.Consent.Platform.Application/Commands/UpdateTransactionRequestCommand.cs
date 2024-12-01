using MediatR;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record UpdateTransactionRequestCommand(int Id, TransactionRequests TransactionRequest) : IRequest<Result<TransactionRequests>>;

public class UpdateTransactionRequestCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdateTransactionRequestCommand, Result<TransactionRequests>>
{
    public async Task<Result<TransactionRequests>> Handle(UpdateTransactionRequestCommand request, CancellationToken ct)
    {
        if (await dbContext.FindAsync<TransactionRequests>(request.Id, ct) is { } found)
        {
            found.DraftDecision = request.TransactionRequest.DraftDecision;
            found.Justification = request.TransactionRequest.Justification;
            found.Status        = request.TransactionRequest.Status;
            found.Requester     = request.TransactionRequest.Requester;
            found.Responder     = request.TransactionRequest.Responder;
        }
        await dbContext.SaveChangesAsync(ct);
        return request.TransactionRequest;
    }
}