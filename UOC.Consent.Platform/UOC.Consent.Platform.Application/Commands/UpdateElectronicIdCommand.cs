using MediatR;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record UpdateElectronicIdCommand(int Id, ElectronicIds ElectronicId) : IRequest<Result<ElectronicIds>>;

public class UpdateElectronicIdCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdateElectronicIdCommand, Result<ElectronicIds>>
{
    public async Task<Result<ElectronicIds>> Handle(UpdateElectronicIdCommand request, CancellationToken ct)
    {
        if (await dbContext.FindAsync<ElectronicIds>(request.Id, ct) is { } found)
        {
            found.Signature          = request.ElectronicId.Signature;
            found.Certification      = request.ElectronicId.Certification;
            found.Seal               = request.ElectronicId.Seal;
            found.BasedOnCertificate = request.ElectronicId.BasedOnCertificate;
        }
        await dbContext.SaveChangesAsync(ct);
        return request.ElectronicId;
    }
}