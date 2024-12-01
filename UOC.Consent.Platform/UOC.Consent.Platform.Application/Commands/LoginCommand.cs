using MediatR;
using UOC.Consent.Platform.Application.Services;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;
using static UOC.Consent.Platform.Shared.DomainError;
using static UOC.Consent.Platform.Shared.Result;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record LoginCommand(string NationalElectronicIdentifier, bool IsEnterprise = false) : IRequest<Result>;

public class LoginCommandHandler(ApplicationDbContext dbContext, IEidasService eidasService)
    : IRequestHandler<LoginCommand, Result>
{
    public async Task<Result> Handle(LoginCommand request, CancellationToken ct)
    {
        var eRecord = await eidasService.GetEidasIdentifierAsync(request.NationalElectronicIdentifier);
        var foundElectronicIds = dbContext
                                 .GetList<ElectronicIds>(x => x.Signature == eRecord.Signature)
                                 .ToList();

        return request.IsEnterprise
            ? foundElectronicIds switch
            {
                { Count: 0 } => Failure(ValidationError("Enterprise not registered")),
                [{ Enterprise : not null } eId] => Success(eId.Enterprise),
                _ => Failure(ValidationError("Inconsistent data, multiples eIDAS for this identifier"))
            }
            : foundElectronicIds switch
            {
                { Count: 0 } => Failure(ValidationError("User not registered")),
                [{ DataSubject: not null } eId] => Success(eId.DataSubject),
                _ => Failure(ValidationError("Inconsistent data, multiples eIDAS for this identifier"))
            };
    }
}