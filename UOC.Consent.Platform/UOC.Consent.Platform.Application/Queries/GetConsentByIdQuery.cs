using MediatR;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetConsentByIdQuery(int Id) : IRequest<Result<Consents>>;

public class GetConsentByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetConsentByIdQuery, Result<Consents>>
{
    public async Task<Result<Consents>> Handle(
        GetConsentByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<Consents>(request.Id);
}