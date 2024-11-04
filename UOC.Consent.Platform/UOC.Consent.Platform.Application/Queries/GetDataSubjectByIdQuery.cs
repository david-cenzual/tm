using MediatR;
using UOC.Consent.Platform.Shared;
using UOC.Consent.Platform.Domain.Users;

namespace UOC.Consent.Platform.Application.Queries;

public sealed record GetDataSubjectByIdQuery(int Id) : IRequest<Result<DataSubject>>;

public class GetDataSubjectByIdQueryHandler : IRequestHandler<GetDataSubjectByIdQuery, Result<DataSubject>>
{
    public async Task<Result<DataSubject>> Handle(GetDataSubjectByIdQuery request, CancellationToken cancellationToken) 
        => DataSubject.GetById(request.Id);
}