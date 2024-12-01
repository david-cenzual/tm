using MediatR;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetLedgersListQuery : IRequest<Result<List<Ledgers>>>;

public class GetLedgerListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetLedgersListQuery, Result<List<Ledgers>>>
{
    public Task<Result<List<Ledgers>>> Handle(GetLedgersListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<Ledgers>().ToList()));
}