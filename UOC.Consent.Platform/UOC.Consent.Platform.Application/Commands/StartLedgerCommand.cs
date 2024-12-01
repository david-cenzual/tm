using MediatR;
using UOC.Consent.Platform.Domain.LedgerAggregate;
using UOC.Consent.Platform.Infrastructure.Services;
using UOC.Consent.Platform.Shared;
using static UOC.Consent.Platform.Shared.Result;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record StartLedgerCommand(Guid ServiceId, Guid UserId, Guid OrgId) : IRequest<Result<Ledgers>>;
