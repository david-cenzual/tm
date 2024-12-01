using MediatR;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetElectronicIdsListQuery : IRequest<Result<List<ElectronicIds>>>;

public class GetElectronicIdListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetElectronicIdsListQuery, Result<List<ElectronicIds>>>
{
    public Task<Result<List<ElectronicIds>>> Handle(GetElectronicIdsListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<ElectronicIds>().ToList()));
}