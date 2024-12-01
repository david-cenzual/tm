using MediatR;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record UpdateEnterpriseCommand(int Id, Enterprises Enterprise) : IRequest<Result<Enterprises>>;

public class UpdateEnterpriseCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdateEnterpriseCommand, Result<Enterprises>>
{
    public async Task<Result<Enterprises>> Handle(UpdateEnterpriseCommand request, CancellationToken ct)
    {
        if (await dbContext.FindAsync<Enterprises>(request.Id, ct) is { } found)
        {
            found.Latitude                    = request.Enterprise.Latitude;
            found.Longitude                   = request.Enterprise.Longitude;
            found.Name                        = request.Enterprise.Name;
            found.IsEngagedInEconomicActivity = request.Enterprise.IsEngagedInEconomicActivity;
            found.LegalForm = request.Enterprise.LegalForm;
        }
        await dbContext.SaveChangesAsync(ct);
        return request.Enterprise;
    }
}