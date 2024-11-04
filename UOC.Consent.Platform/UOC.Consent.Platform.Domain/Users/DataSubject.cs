using System.Diagnostics.CodeAnalysis;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.Domain.Users;

[method: SetsRequiredMembers]
public class DataSubject(int id, Guid reference, ElectronicId electronicId, List<ConsentAggregate.Consent> consents)
{
    public required int                            Id           { get; set; }         = id;
    public          Guid                           Reference    { get; private set; } = Guid.NewGuid();
    public required ElectronicId                   ElectronicId { get; set; }         = electronicId;
    public          List<ConsentAggregate.Consent> Consents     { get; set; }         = consents;

    public static Result<DataSubject> GetById(int id) => Result
        .Success(
            new DataSubject(
                id, 
                Guid.NewGuid(),
                new ElectronicId("","","",""), 
                []));
}