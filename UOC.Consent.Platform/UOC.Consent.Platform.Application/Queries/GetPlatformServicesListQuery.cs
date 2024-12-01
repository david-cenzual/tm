using MediatR;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetPlatformServicesListQuery : IRequest<Result<List<PlatformServices>>>;

public class GetPlatformServiceListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetPlatformServicesListQuery, Result<List<PlatformServices>>>
{
    public Task<Result<List<PlatformServices>>> Handle(GetPlatformServicesListQuery request, CancellationToken ct)
        => Task.FromResult(Result.Success(dbContext.GetList<PlatformServices>().ToList()));
}