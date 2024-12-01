using MediatR;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateConsentCommand(Consents Consent) : IRequest<Result<Consents>>;

public class CreateConsentCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateConsentCommand, Result<Consents>>
{
    public async Task<Result<Consents>> Handle(CreateConsentCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.Consent);
        await dbContext.SaveChangesAsync(ct);
        return request.Consent;
    }
}