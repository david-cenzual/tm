using MediatR;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetTransactionRequestByIdQuery(int Id) : IRequest<Result<TransactionRequests>>;

public class GetTransactionRequestByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTransactionRequestByIdQuery, Result<TransactionRequests>>
{
    public async Task<Result<TransactionRequests>> Handle(
        GetTransactionRequestByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<TransactionRequests>(request.Id);
}