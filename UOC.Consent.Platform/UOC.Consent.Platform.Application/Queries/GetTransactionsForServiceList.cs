using MediatR;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetTransactionsForServiceListQuery(int ServiceId) : IRequest<Result<List<Transactions>>>;

public class GetTransactionsForServiceListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTransactionsForServiceListQuery, Result<List<Transactions>>>
{
    public Task<Result<List<Transactions>>> Handle(GetTransactionsForServiceListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<Transactions>(x => x.Service?.Id == request.ServiceId).ToList()));
}