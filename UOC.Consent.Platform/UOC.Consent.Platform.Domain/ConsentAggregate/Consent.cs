namespace UOC.Consent.Platform.Domain.ConsentAggregate;

public class Consent(
    int id, 
    Guid dataSubjectRef, 
    Guid enterpriseRef, 
    Guid serviceRef, 
    bool isRequired, 
    DateTime dateGiven, 
    string? processingPurpose, 
    string? supportingInformation,
    ConsentStatus status,
    ConsentMethod method,
    PersonalDataIdentifier identifier)
{
    public int Id { get; set; } = id;

    public Guid DataSubjectReference { get; set; } = dataSubjectRef;
    public Guid EnterpriseReference  { get; set; } = enterpriseRef;
    public Guid ServiceReference     { get; set; } = serviceRef;

    public bool IsRequiredByService { get; set; } = isRequired;

    public DateTime DateGiven { get; set; } = dateGiven;

    public string? ProcessingPurpose     { get; set; } = processingPurpose;
    public string? SupportingInformation { get; set; } = supportingInformation;

    public ConsentStatus          ConsentStatus { get; set; } = status;
    public ConsentMethod          Method        { get; set; } = method;
    public PersonalDataIdentifier Identifier    { get; set; } = identifier;
}