namespace UOC.Consent.Platform.Domain.TransactionRequestAggregate;

public class TransactionRequest(
    int id,
    Objection? objection,
    List<ConsentAggregate.Consent> consents,
    TransactionRequestStatus status,
    Guid requester,
    Guid responder,
    Guid ledgerRef,
    Guid enterpriseRef,
    Guid dataSubjectRef,
    Guid serviceRef)
{
    public int  Id        { get; set; }         = id;
    public Guid Reference { get; private set; } = Guid.NewGuid();
    public Objection?                     Objection            { get; set; } = objection;
    public List<ConsentAggregate.Consent> Consents             { get; set; } = consents;
    public TransactionRequestStatus       Status               { get; set; } = status;
    public Guid                           Requester            { get; set; } = requester;
    public Guid                           Responder            { get; set; } = responder;
    public Guid                           LedgerReference      { get; set; } = ledgerRef;
    public Guid                           EnterpriseReference  { get; set; } = enterpriseRef;
    public Guid                           DataSubjectReference { get; set; } = dataSubjectRef;
    public Guid                           ServiceReference     { get; set; } = serviceRef;
}