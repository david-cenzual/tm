using MediatR;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetConsentsListQuery : IRequest<Result<List<Consents>>>;

public class GetConsentListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetConsentsListQuery, Result<List<Consents>>>
{
    public Task<Result<List<Consents>>> Handle(GetConsentsListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<Consents>().ToList()));
}