using MediatR;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateEnterpriseCommand(Enterprises Enterprise) : IRequest<Result<Enterprises>>;

public class CreateEnterpriseCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateEnterpriseCommand, Result<Enterprises>>
{
    public async Task<Result<Enterprises>> Handle(CreateEnterpriseCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.Enterprise);
        await dbContext.SaveChangesAsync(ct);
        return request.Enterprise;
    }
}