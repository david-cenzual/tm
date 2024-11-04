using MediatR;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Shared;
using static UOC.Consent.Platform.Shared.Result;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record StartLedgerCommand(Guid ServiceId, Guid UserId, Guid OrgId) : IRequest<Result<Ledger>>;

public class StartLedgerCommandHandler : IRequestHandler<StartLedgerCommand, Result<Ledger>>
{
    public async Task<Result<Ledger>> Handle(StartLedgerCommand request, CancellationToken cancellationToken)
    {
        var result = await Ledger.FindLedger("", "", "");
        
        return result.IsSuccess
            ? Failure<Ledger>(DomainError.ValidationError("Ledger already exists"))
            : Ledger.Start(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
    }
}