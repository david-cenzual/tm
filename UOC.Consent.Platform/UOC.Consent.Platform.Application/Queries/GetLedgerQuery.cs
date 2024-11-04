using MediatR;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetLedgerQuery(Guid LedgerId) : IRequest<Result<Ledger>>;

public class GetLedgerQueryHandler : IRequestHandler<GetLedgerQuery, Result<Ledger>>
{
    public async Task<Result<Ledger>> Handle(GetLedgerQuery request, CancellationToken cancellationToken) 
        => await Ledger.GetLedgerById(request.LedgerId);
}