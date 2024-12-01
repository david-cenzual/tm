using MediatR;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateElectronicIdCommand(ElectronicIds ElectronicId) : IRequest<Result<ElectronicIds>>;

public class CreateElectronicIdCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateElectronicIdCommand, Result<ElectronicIds>>
{
    public async Task<Result<ElectronicIds>> Handle(CreateElectronicIdCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.ElectronicId);
        await dbContext.SaveChangesAsync(ct);
        return request.ElectronicId;
    }
}