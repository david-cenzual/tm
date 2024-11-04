namespace UOC.Consent.Platform.Domain.EnterpriseServiceAggregate;

public class EnterpriseService(
    int id,
    string name,
    bool isRemuneration,
    bool isDistance,
    bool isElectronic,
    bool isRequested)
{
    public int  ServiceId { get; private set; } = id;
    public Guid Reference { get; private set; } = Guid.NewGuid();
    public string Name                          { get; private set; } = name;
    public bool   IsProvidedForRemuneration     { get; private set; } = isRemuneration;
    public bool   IsProvidedAtADistance         { get; private set; } = isDistance;
    public bool   IsProvidedByElectronicMeans   { get; private set; } = isElectronic;
    public bool   IsProvidedAtIndividualRequest { get; private set; } = isRequested;
}