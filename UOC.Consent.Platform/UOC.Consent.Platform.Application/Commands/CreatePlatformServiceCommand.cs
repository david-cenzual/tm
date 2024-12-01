using MediatR;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreatePlatformServiceCommand(PlatformServices PlatformService) : IRequest<Result<PlatformServices>>;

public class CreatePlatformServiceCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreatePlatformServiceCommand, Result<PlatformServices>>
{
    public async Task<Result<PlatformServices>> Handle(CreatePlatformServiceCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.PlatformService);
        await dbContext.SaveChangesAsync(ct);
        return request.PlatformService;
    }
}