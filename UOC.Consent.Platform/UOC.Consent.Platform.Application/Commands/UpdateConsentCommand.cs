using MediatR;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record UpdateConsentCommand(int Id, Consents Consent) : IRequest<Result<Consents>>;

public class UpdateConsentCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdateConsentCommand, Result<Consents>>
{
    public async Task<Result<Consents>> Handle(UpdateConsentCommand request, CancellationToken ct)
    {
        if (await dbContext.FindAsync<Consents>(request.Id, ct) is { } found)
        {
            found.IsRequired            = request.Consent.IsRequired;
            found.DateGiven             = request.Consent.DateGiven;
            found.ProcessingPurpose     = request.Consent.ProcessingPurpose;
            found.SupportingInformation = request.Consent.SupportingInformation;
            found.Status                = request.Consent.Status;
            found.Method                = request.Consent.Method;
            found.DataIdentifier        = request.Consent.DataIdentifier;
        }

        await dbContext.TryUpdate([request.Consent.Id], request.Consent, ct);
        await dbContext.SaveChangesAsync(ct);
        return request.Consent;
    }
}