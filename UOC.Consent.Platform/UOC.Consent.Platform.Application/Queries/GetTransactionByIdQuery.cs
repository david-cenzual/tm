using MediatR;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetTransactionByIdQuery(int Id) : IRequest<Result<Transactions>>;

public class GetTransactionByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTransactionByIdQuery, Result<Transactions>>
{
    public async Task<Result<Transactions>> Handle(
        GetTransactionByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<Transactions>(request.Id);
}