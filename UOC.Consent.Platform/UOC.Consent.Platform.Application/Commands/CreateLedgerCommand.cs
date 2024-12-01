using MediatR;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateLedgerCommand(Ledgers Ledger) : IRequest<Result<Ledgers>>;

public class CreateLedgerCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateLedgerCommand, Result<Ledgers>>
{
    public async Task<Result<Ledgers>> Handle(CreateLedgerCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.Ledger);
        await dbContext.SaveChangesAsync(ct);
        return request.Ledger;
    }
}