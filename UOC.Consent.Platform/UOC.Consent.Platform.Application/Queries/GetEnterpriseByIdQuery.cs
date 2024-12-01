using MediatR;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetEnterpriseByIdQuery(int Id) : IRequest<Result<Enterprises>>;

public class GetEnterpriseByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetEnterpriseByIdQuery, Result<Enterprises>>
{
    public async Task<Result<Enterprises>> Handle(
        GetEnterpriseByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<Enterprises>(request.Id);
}