using MediatR;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateTransactionCommand(Transactions Transaction) : IRequest<Result<Transactions>>;

public class CreateTransactionCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateTransactionCommand, Result<Transactions>>
{
    public async Task<Result<Transactions>> Handle(CreateTransactionCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.Transaction);
        await dbContext.SaveChangesAsync(ct);
        return request.Transaction;
    }
}