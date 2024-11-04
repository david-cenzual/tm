namespace UOC.Consent.Platform.Domain.Users;

/// <summary>
/// eIDAs identification
/// </summary>
public class ElectronicId(string signature, string certification, string seal, string basedOnCertificate)
{
    public string Signature          { get; set; } = signature;
    public string Certification      { get; set; } = certification;
    public string Seal               { get; set; } = seal;
    public string BasedOnCertificate { get; set; } = basedOnCertificate;
}