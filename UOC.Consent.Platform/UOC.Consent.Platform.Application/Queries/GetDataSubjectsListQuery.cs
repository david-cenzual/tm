using MediatR;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Infrastructure.DatabaseContext.Operations;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetDataSubjectsListQuery : IRequest<Result<List<DataSubjects>>>;

public class GetDataSubjectListQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetDataSubjectsListQuery, Result<List<DataSubjects>>>
{
    public Task<Result<List<DataSubjects>>> Handle(GetDataSubjectsListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Result.Success(dbContext.GetList<DataSubjects>().ToList()));
}