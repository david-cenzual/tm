using MediatR;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetElectronicIdByIdQuery(int Id) : IRequest<Result<ElectronicIds>>;

public class GetElectronicIdByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetElectronicIdByIdQuery, Result<ElectronicIds>>
{
    public async Task<Result<ElectronicIds>> Handle(
        GetElectronicIdByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<ElectronicIds>(request.Id);
}