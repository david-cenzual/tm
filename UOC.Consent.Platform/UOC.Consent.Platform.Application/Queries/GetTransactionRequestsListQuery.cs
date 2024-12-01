using MediatR;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetTransactionRequestsListQuery : IRequest<Result<List<TransactionRequests>>>;

public class GetTransactionRequestListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTransactionRequestsListQuery, Result<List<TransactionRequests>>>
{
    public Task<Result<List<TransactionRequests>>> Handle(GetTransactionRequestsListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<TransactionRequests>().ToList()));
}