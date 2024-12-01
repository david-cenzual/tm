using MediatR;
using UOC.Consent.Platform.Application.Services;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;
using static UOC.Consent.Platform.Shared.DomainError;
using static UOC.Consent.Platform.Shared.Result;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record RegisterDataSubjectCommand(string NationalElectronicIdentifier) : IRequest<Result>;

public class RegisterDataSubjectCommandHandler(ApplicationDbContext dbContext, IEidasService eidasService)
    : IRequestHandler<RegisterDataSubjectCommand, Result>
{
    public async Task<Result> Handle(RegisterDataSubjectCommand request, CancellationToken ct)
    {
        var eRecord = await eidasService.GetEidasIdentifierAsync(request.NationalElectronicIdentifier);
        var foundElectronicIds = dbContext
                                 .GetList<ElectronicIds>(x => x.Signature == eRecord.Signature)
                                 .ToList();

        return foundElectronicIds switch
        {
            { Count: 0 } => Success(await dbContext.RegisterDataSubjectWithNewEidas(eRecord).SaveChangesAsync(ct)),
            [{ DataSubject: not null } eId] => Success(await dbContext.RegisterDataSubjectWithExistingEidas(eId).SaveChangesAsync(ct)),
            _ => Failure(ValidationError($"Already registered eIDAS: ({request.NationalElectronicIdentifier})"))
        };
    }
}