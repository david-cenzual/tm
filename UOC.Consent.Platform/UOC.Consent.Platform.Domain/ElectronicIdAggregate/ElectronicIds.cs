using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;

namespace UOC.Consent.Platform.Domain.ElectronicIdAggregate;

/// <summary>
/// eIDAs identification
/// </summary>
public class ElectronicIds
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Reference { get; set; }

    [MaxLength(255)] 
    public string? Signature          { get; set; }
    
    [MaxLength(255)] 
    public string? Certification      { get; set; }
    
    [MaxLength(255)] 
    public string? Seal               { get; set; }
    
    [MaxLength(255)] 
    public string? BasedOnCertificate { get; set; }

    public virtual DataSubjects? DataSubject { get; set; }
    public virtual Enterprises?  Enterprise  { get; set; }
}