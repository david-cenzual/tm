using MediatR;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetDataSubjectByIdQuery(int Id) : IRequest<Result<DataSubjects>>;

public class GetDataSubjectByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetDataSubjectByIdQuery, Result<DataSubjects>>
{
    public async Task<Result<DataSubjects>> Handle(
        GetDataSubjectByIdQuery request,
        CancellationToken cancellationToken)
        => await dbContext.GetByPk<DataSubjects>(request.Id);
}