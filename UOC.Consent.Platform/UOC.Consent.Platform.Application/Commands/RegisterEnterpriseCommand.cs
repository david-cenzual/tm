using MediatR;
using UOC.Consent.Platform.Application.Services;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;
using static UOC.Consent.Platform.Shared.DomainError;
using static UOC.Consent.Platform.Shared.Result;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record RegisterEnterpriseCommand(
    string NationalElectronicIdentifier, 
    Enterprises Enterprise) : IRequest<Result>;

public class RegisterEnterpriseCommandHandler(ApplicationDbContext dbContext, IEidasService eidasService)
    : IRequestHandler<RegisterEnterpriseCommand, Result>
{
    public async Task<Result> Handle(RegisterEnterpriseCommand request, CancellationToken ct)
    {
        var eRecord = await eidasService.GetEidasIdentifierAsync(request.NationalElectronicIdentifier);
        var foundElectronicIds = dbContext
                                 .GetList<ElectronicIds>(x => x.Signature == eRecord.Signature)
                                 .ToList();

        return foundElectronicIds switch
        {
            { Count: 0 } => Success(await dbContext.RegisterEnterpriseWithNewEidas(eRecord).SaveChangesAsync(ct)),
            [{ Enterprise: not null } and {DataSubject: not null} eId] => Success(await dbContext.RegisterEnterpriseWithExistingEidas(eId).SaveChangesAsync(ct)),
            _ => Failure(ValidationError($"Already registered eIDAS: ({request.NationalElectronicIdentifier})"))
        };
    }
}