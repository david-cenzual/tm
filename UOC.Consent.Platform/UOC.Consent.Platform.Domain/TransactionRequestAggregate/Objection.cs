namespace UOC.Consent.Platform.Domain.TransactionRequestAggregate;

public class Objection(DraftDecision draftDecision, string justification)
{
    public DraftDecision DraftDecision { get; set; } = draftDecision;
    public string        Justification { get; set; } = justification;
}