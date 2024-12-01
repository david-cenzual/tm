using MediatR;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetEnterprisesListQuery : IRequest<Result<List<Enterprises>>>;

public class GetEnterpriseListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetEnterprisesListQuery, Result<List<Enterprises>>>
{
    public Task<Result<List<Enterprises>>> Handle(GetEnterprisesListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<Enterprises>().ToList()));
}