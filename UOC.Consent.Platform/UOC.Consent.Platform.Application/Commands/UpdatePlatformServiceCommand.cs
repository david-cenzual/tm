using MediatR;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record UpdatePlatformServiceCommand(int Id, PlatformServices PlatformService) : IRequest<Result<PlatformServices>>;

public class UpdateEnterpriseServiceCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdatePlatformServiceCommand, Result<PlatformServices>>
{
    public async Task<Result<PlatformServices>> Handle(UpdatePlatformServiceCommand request, CancellationToken ct)
    {
        if (await dbContext.FindAsync<PlatformServices>(request.Id, ct) is { } found)
        {
            found.Name           = request.PlatformService.Name;
            found.IsRemuneration = request.PlatformService.IsRemuneration;
            found.IsDistance     = request.PlatformService.IsDistance;
            found.IsElectronic   = request.PlatformService.IsElectronic;
            found.IsRequested    = request.PlatformService.IsRequested;
        }
        await dbContext.SaveChangesAsync(ct);
        return request.PlatformService;
    }
}