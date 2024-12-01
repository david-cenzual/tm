using MediatR;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Infrastructure.DatabaseContext;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Application.Commands;

public sealed record CreateDataSubjectCommand(DataSubjects DataSubject) : IRequest<Result<DataSubjects>>;

public class CreateDataSubjectCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateDataSubjectCommand, Result<DataSubjects>>
{
    public async Task<Result<DataSubjects>> Handle(CreateDataSubjectCommand request, CancellationToken ct)
    {
        dbContext.AddRange(request.DataSubject);
        await dbContext.SaveChangesAsync(ct);
        return request.DataSubject;
    }
}