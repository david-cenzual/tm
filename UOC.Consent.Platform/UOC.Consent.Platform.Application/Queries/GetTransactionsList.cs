using MediatR;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetTransactionsListQuery : IRequest<Result<List<Transactions>>>;

public class GetTransactionListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTransactionsListQuery, Result<List<Transactions>>>
{
    public Task<Result<List<Transactions>>> Handle(GetTransactionsListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<Transactions>().ToList()));
}