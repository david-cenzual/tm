namespace UOC.Consent.Platform.Domain.Users;

public class Enterprise(
    int id, 
    ElectronicId electronicId, 
    string name, 
    LegalForm legalForm, 
    bool isEngagedInEconomicActivity,
    double latitude,
    double longitude)
{
    public required int  Id        { get; set; }         = id;
    public          Guid Reference { get; private set; } = Guid.NewGuid();
    public required ElectronicId ElectronicId                { get; set; } = electronicId;
    public          string       Name                        { get; set; } = name;
    public          LegalForm    LegalForm                   { get; set; } = legalForm;
    public          bool         IsEngagedInEconomicActivity { get; set; } = isEngagedInEconomicActivity;
    public          double       LatitudeCoordinate          { get; set; } = latitude;
    public          double       LongitudeCoordinate         { get; set; } = longitude;
}