using System.Security.Cryptography;
using System.Text;

namespace UOC.Consent.Platform.Domain.Transaction;

public class Transaction
{
    public int  Id        { get; private set; }
    public Guid Reference { get; private set; } = Guid.NewGuid();
    public DateTime TimeStamp            { get; private set; }
    public Guid     LedgerReference      { get; set; }
    public Guid     EnterpriseReference  { get; set; }
    public Guid     DataSubjectReference { get; set; }
    public Guid     ServiceReference     { get; set; }
    public string   PreviousHash         { get; private set; }
    public string   Hash                 { get; private set; }
    public string   Data                 { get; private set; }

    public Transaction(
        int id, 
        string previousHash, 
        string data, 
        Guid ledgerReference, 
        Guid enterpriseReference, 
        Guid dataSubjectReference, 
        Guid serviceReference)
    {
        Id                   = id;
        LedgerReference      = ledgerReference;
        EnterpriseReference  = enterpriseReference;
        DataSubjectReference = dataSubjectReference;
        ServiceReference     = serviceReference;
        TimeStamp            = DateTime.UtcNow;
        PreviousHash         = previousHash;
        Data                 = data;
        Hash                 = CalculateHash();
    }

    public string CalculateHash()
    {
        var rawData = $"{Id}{TimeStamp}{PreviousHash}{Data}";
        var bytes   = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}