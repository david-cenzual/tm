using MediatR;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetLedgerByIdQuery() : IRequest<Result<Ledgers>>;

public class GetLedgerByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetLedgerByIdQuery, Result<Ledgers>>
{
    public async Task<Result<Ledgers>> Handle(
        GetLedgerByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.FindAsync<Ledgers>() ?? Result.Failure<Ledgers>(DomainError.UnexpectedError("No ledger found"));
}