using MediatR;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetPlatformServiceByIdQuery(int Id) : IRequest<Result<PlatformServices>>;

public class GetPlatformServiceByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetPlatformServiceByIdQuery, Result<PlatformServices>>
{
    public async Task<Result<PlatformServices>> Handle(
        GetPlatformServiceByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<PlatformServices>(request.Id);
}