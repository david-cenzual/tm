using MediatR;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateTransactionRequestCommand(TransactionRequests TransactionRequest) : IRequest<Result<TransactionRequests>>;

public class CreateTransactionRequestCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateTransactionRequestCommand, Result<TransactionRequests>>
{
    public async Task<Result<TransactionRequests>> Handle(CreateTransactionRequestCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.TransactionRequest);
        await dbContext.SaveChangesAsync(ct);
        return request.TransactionRequest;
    }
}