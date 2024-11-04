namespace UOC.Consent.Platform.BoundedContext

open System

// Represents different types of identifiers that can be used to identify a natural person
type Identifier =
    | Name                  of string
    | IdentificationNumber  of string
    | LocationData          of string
    | OnlineIdentifier      of string
    | PhysicalFactor        of string // Revise this
    | PhysiologicalFactor   of string // Revise this
    | GeneticFactor         of string // Revise this
    | MentalFactor          of string // Revise this < - Strange
    | EconomicFactor        of string
    | CulturalFactor        of string
    | SocialFactor          of string

type ProcessingOperation =
    | Collection                of string
    | Recording                 of string
    | Organisation              of string
    | Structuring               of string
    | Storage                   of string
    | Adaptation                of string
    | Alteration                of string
    | Retrieval                 of string
    | Consultation              of string
    | Use                       of string
    | DisclosureByTransmission  of string
    | Dissemination             of string
    | MakingAvailable           of string
    | Alignment                 of string
    | Combination               of string
    | Restriction               of string
    | Erasure                   of string
    | Destruction               of string

type RestrictionStatus = Restricted | Unrestricted

type PersonalData = {
    DataSubjectReference: Guid
    Identifier:     Identifier          // The personal data being marked for restriction
    Reason:         string              // The reason for restricting the data
    IsRestricted:   RestrictionStatus   // Indicates whether the data is currently restricted
    // Maybe needs a Data Field with the types of data
    // For this patient I want to upload data here (?)
    // So that an enterprise can search the data here
    // That means that a Processor can request this data or upload it
}

// A record representing the concept of a natural person
// Is Entity
type DataSubject = {
    Id:             int                // Unique identifier for the data subject
    Reference:      string
    eIDAS:          string
    Information:    PersonalData list   // List of identifiers for the data subject
}

type AutomationType = Automated | Manual

// A record representing the processing of personal data
type Processing = {
    Operations:     ProcessingOperation list    // List of operations being performed on the data
    AutomationType: AutomationType              // Indicates whether the processing is automated or manual
    TargetData:     PersonalData                // The personal data being processed
}

// Represents different aspects of a natural person that can be evaluated or predicted during profiling
type ProfilingAspect =
    | WorkPerformance
    | EconomicSituation
    | Health
    | PersonalPreferences
    | Interests
    | Reliability
    | Behaviour
    | Location
    | Movements

// A record representing the concept of profiling
type Profiling = {
    TargetData:         PersonalData          // The personal data used for profiling
    EvaluatedAspects:   ProfilingAspect list  // The aspects being evaluated or predicted
    AutomationType:     AutomationType        // Indicates if the profiling is automated
}

type FilingSystemType =
    | Centralised
    | Decentralised
    | DispersedByFunction
    | DispersedByGeography

type FilingSystem = {
    StoredData:     PersonalData list       // The structured set of personal data in the system
    AccessCriteria: string                  // The criteria by which the data is accessible
    SystemType:     FilingSystemType        // The structure type of the filing system
}

type OperatorType =
    | NaturalPerson of string   // e.g., Name of the person
    | LegalEntity of string     // e.g., Name of the company or organization
    | PublicAuthority of string // e.g., Name of the authority
    | OtherBody of string       // e.g., Name of the agency or other body

type ProcessingType = Alone | Jointly

// A record representing the controller
// Is Entity
type Controller = {
    Id: int
    MasterControllerType: OperatorType          // The entity determining the purposes and means of processing
    DeterminesProcessing: ProcessingType                  // Indicates whether the entity determines processing alone or jointly
    JointControllers: OperatorType list option  // Optional list of other controllers if processing is determined jointly
    LegalBasis: string option                   // Optional legal basis if the controller is determined by law (e.g., Union or Member State law)
}

// Is Entity
type Processor = {
    Id: int
    MasterProcessorEntity: OperatorType     // The entity processing personal data on behalf of the controller
    Controller: Controller                  // The controller on whose behalf the processor is acting
    Operations: ProcessingOperation list    // List of standard processing operations
    ProcessingTasks: string list            // List of tasks the processor performs on behalf of the controller
}

type Recipient =
    | RegularRecipient of OperatorType          // A regular recipient to whom personal data is disclosed
    | PublicAuthorityInquiry of OperatorType    // Public authorities that receive data as part of a legal inquiry
    
type ThirdParty =
    | ThirdPartyEntity of OperatorType   // A third party who is neither the data subject, controller, nor processor
    | AuthorizedEntity of OperatorType   // An entity that is authorized by the controller or processor to process personal data and is not considered a third party

type ConsentMethod =
    | Statement of string       // e.g., A written or spoken statement by the data subject
    | AffirmativeAction of string // e.g., A clear action indicating consent, such as ticking a box or signing a form

type ConsentStatus =
    | Given
    | Requested
    | Denied

// A record representing the consent of a data subject
// Is Entity
type Consent = {
    Id: int
    DataSubjectReference: Guid     // Identifier of the data subject giving consent
    EnterpriseReference : Guid
    Required: bool
    ConsentStatus: ConsentStatus   // Whether the consent is given or denied
    Method: ConsentMethod          // The way consent was given (via statement or affirmative action)
    DateGiven: DateTime            // The date the consent was given
    ProcessingPurpose: string      // The specific purpose for which the consent is given
}

// Represents the different types of personal data breaches
type BreachType =
    | Destruction        // Accidental or unlawful destruction of personal data
    | Loss               // Accidental or unlawful loss of personal data
    | Alteration         // Unlawful or unauthorized alteration of personal data
    | UnauthorizedDisclosure // Unauthorized disclosure of personal data to a third party
    | UnauthorizedAccess  // Unauthorized access to personal data by an unauthorized person

type BreachVulnerability = Accidental | Unlawful

// A record representing a personal data breach
// TODO: A data breach should affect a list of organizations
// TODO: A data breach should affect a portion or all data
type PersonalDataBreach = {
    BreachId:           int                    // Unique identifier for the breach
    BreachType:         BreachType              // Type of breach (destruction, loss, alteration, etc.)
    AffectedData:       Identifier list         // List of personal data items affected by the breach
    VulnerabilityType:  BreachVulnerability     // Indicates whether the breach was accidental or unlawful
    DateOfBreach:       DateTime                // The date the breach occurred
    DiscoveredOn:       DateTime                // The date the breach was discovered
    Description:        string option           // Optional description of the breach details
}

// Represents the source or type of biological sample analyzed to obtain genetic data
type BiologicalSample =
    | Blood
    | Saliva
    | Tissue
    | Hair
    | Other of string  // Allows flexibility for other types of samples

// A record representing genetic data
type GeneticData = {
    DataSubjectReference:          Guid
    BiologicalSampleType:   BiologicalSample    // The type of biological sample used for analysis
    DateOfAnalysis:         DateTime            
}

// Represents different types of biometric data used for unique identification
type BiometricType =
    | FacialImage             // Biometric data from facial recognition
    | Fingerprint             // Dactyloscopic (fingerprint) data
    | IrisScan                // Iris or retinal scan data
    | VoicePattern            // Voice recognition data
    | GaitPattern             // Data based on a person's walking pattern
    | Other of string         // Flexibility for other biometric types not covered explicitly

type IdentifiableData = Identifiable | Unidentifiable 

type BiometricData = {
    DataSubjectReference: Guid                // Identifier of the natural person whose biometric data is being processed
    BiometricType: BiometricType       // The type of biometric data (facial, fingerprint, etc.)
    DateOfProcessing: DateTime         // The date when the biometric data was processed
    ProcessingMethod: string           // Description of the technical method used for processing the biometric data
    UniqueIdentification: IdentifiableData         // Indicates whether the data allows for unique identification of the individual
}

type HealthDataSource =
    | MedicalRecord         // Data from official medical records
    | HealthcareProvider    // Data provided by a healthcare professional or institution
    | SelfReported          // Health data reported by the individual themselves
    | WearableDevice        // Data collected by health-related wearable devices (e.g., fitness trackers)
    | Other of string       // Flexibility for other sources not covered explicitly

// A record representing health-related data
type HealthData = {
    DataSubjectReference: Guid               // Identifier of the natural person whose health data is being processed
    PhysicalHealth: string option       // Optional information related to the person's physical health
    MentalHealth: string option         // Optional information related to the person's mental health
    HealthCareServices: string option   // Information about the provision of healthcare services
    HealthDataSource: HealthDataSource  // The source of the health data (e.g., medical record, wearable device)
    DateOfData: DateTime         // The date the health data was collected or recorded
    HealthStatusSummary: string option  // Optional summary providing an overall assessment of health status
}

// Represents the type of entity (Controller or Processor)
type EntityType =
    | Controller
    | Processor

// A record representing the main establishment for controllers
type ControllerMainEstablishment = {
    CentralAdministration: string     // The location of the central administration in the Union
    DecisionMakingEstablishment: string option // Optional, the establishment where decisions on processing are taken (if different)
    IsDecisionMakingPower: bool       // Indicates whether the decision-making establishment has power to implement decisions
}

// A record representing the main establishment for processors
type ProcessorMainEstablishment = {
    CentralAdministration: string option    // The location of central administration in the Union, if any
    MainProcessingEstablishment: string     // The establishment in the Union where the main processing activities take place
    IsSubjectToObligations: bool            // Indicates whether the processor is subject to specific obligations under the regulation
}

// A discriminated union to represent the main establishment for both controllers and processors
type MainEstablishment =
    | ControllerEstablishment of ControllerMainEstablishment
    | ProcessorEstablishment of ProcessorMainEstablishment

type RepresentedEntity =
    | Controller
    | Processor

// A record representing the representative of a controller or processor
type Representative = {
    RepresentativeId: int                  // Identifier for the representative (natural or legal person)
    Name: string                            // Name of the representative
    IsLegalPerson: bool                     // Indicates whether the representative is a legal person (true) or a natural person (false)
    EstablishedInUnion: string              // The location of the representative's establishment within the Union
    DesignatedBy: RepresentedEntity         // Indicates whether the representative is designated by a controller or processor
    DesignationDocument: string             // Reference to the written designation pursuant to Article 27
}

// Represents the legal form of an enterprise
type LegalForm =
    | SoleProprietorship     // A natural person engaged in economic activity
    | Partnership            // A partnership regularly engaged in economic activity
    | Corporation            // A legal entity (e.g., company, corporation)
    | Association            // An association engaged in economic activity
    | Other of string        // Flexibility for other legal forms not explicitly listed

// A record representing an enterprise
type Enterprise = {
    EnterpriseId: string          // Unique identifier for the enterprise (could be registration number or similar)
    Name: string                  // Name of the enterprise
    LegalForm: LegalForm          // The legal form of the enterprise
    EngagedInEconomicActivity: bool // Boolean indicating that the entity is engaged in economic activity
    Location: string              // Location where the enterprise operates
}

// Represents an individual undertaking (either controlling or controlled)
type Undertaking = {
    UndertakingId: string           // Unique identifier for the undertaking
    Name: string                    // Name of the undertaking
    IsControlling: bool             // Indicates whether this undertaking is the controlling entity
    Location: string                // Location of the undertaking (e.g., country or headquarters)
}

// A record representing a group of undertakings
type GroupOfUndertakings = {
    ControllingUndertaking: Undertaking   // The controlling undertaking in the group
    ControlledUndertakings: Undertaking list // List of controlled undertakings within the group
}

// Represents the type of entity involved in BCRs (Controller or Processor)
type BindingCorporateRulesEntityType =
    | Controller
    | Processor

// Represents the geographical scope of the BCRs (EU Member State or Third Country)
type GeographicScope =
    | MemberState of string           // The Member State where the controller/processor is established
    | ThirdCountry of string          // A third country outside the EU where the data is being transferred

// A record representing Binding Corporate Rules (BCRs)
type BindingCorporateRules = {
    BCRId: int                      // Unique identifier for the BCR
    AdheringEntity: BindingCorporateRulesEntityType       // The entity (Controller or Processor) adhering to the BCRs
    EstablishedIn: GeographicScope      // The geographical scope where the entity is established (Member State or Third Country)
    TransferringTo: GeographicScope list // List of third countries where personal data is transferred under the BCRs
    GroupStructure: string              // Information about the group of undertakings or enterprises engaged in joint economic activity
    DataProtectionPolicies: string      // The personal data protection policies being adhered to
}

// A record representing a Supervisory Authority
type SupervisoryAuthority = {
    AuthorityId: int                // Unique identifier for the supervisory authority
    Name: string                       // Name of the supervisory authority
    MemberState: string                // The Member State where the authority is established
    IsIndependent: bool                // Boolean indicating that the authority is independent
    EstablishedPursuantTo: string      // Reference to Article 51 (or other applicable legal basis)
}

// Represents the reason why a supervisory authority is concerned
type ConcernReason =
    | ControllerOrProcessorEstablished   // The controller or processor is established in the Member State of the authority
    | DataSubjectsSubstantiallyAffected  // Data subjects in the authority's Member State are substantially affected by the processing
    | ComplaintLodged                    // A complaint has been lodged with the authority

// A record representing a Supervisory Authority Concerned
type SupervisoryAuthorityConcerned = {
    SupervisoryAuthority: SupervisoryAuthority // The supervisory authority concerned
    ConcernReason: ConcernReason              // The reason why the authority is concerned
}

// Represents the type of cross-border processing scenario
type CrossBorderProcessingType =
    | MultipleEstablishments            // Processing occurs in multiple Member States
    | SingleEstablishmentAffectingMultipleStates // Processing occurs in a single establishment but affects multiple Member States

// A record representing Cross-Border Processing
type CrossBorderProcessing = {
    ControllerOrProcessorId: int       // The identifier or name of the controller or processor
    ProcessingType: CrossBorderProcessingType // The type of cross-border processing scenario
    AffectedMemberStates: string list   // List of Member States affected by the processing
    SubstantiallyAffectsDataSubjects: bool // Indicates if data subjects in multiple states are substantially affected
}

// Represents the type of draft decision being objected to
type DraftDecisionType =
    | Infringement                       // Decision relates to whether there is an infringement of the regulation
    | ComplianceWithRegulation           // Decision relates to whether the action complies with the regulation

// Represents a relevant and reasoned objection
type RelevantAndReasonedObjection = {
    ObjectionId: string                  // Unique identifier for the objection
    DraftDecision: DraftDecisionType     // Type of draft decision being objected to
    RisksToRightsAndFreedoms: string     // Description of the significance of the risks to the fundamental rights and freedoms of data subjects
    AffectsFreeFlowOfData: bool          // Indicates whether the decision affects the free flow of personal data within the Union
    Justification: string                // Clear reasoning that supports the objection
}

// Represents an Information Society Service
type EnterpriseService = {
    ServiceId: int                     // Unique identifier for the service
    Name: string                        // Name of the service
    IsProvidedForRemuneration: bool     // Indicates if the service is normally provided for remuneration
    IsProvidedAtADistance: bool         // Indicates if the service is provided at a distance
    IsProvidedByElectronicMeans: bool   // Indicates if the service is provided by electronic means
    IsProvidedAtIndividualRequest: bool // Indicates if the service is provided at the individual request of the recipient
}

// Represents an International Organization
type InternationalOrganization = {
    OrganizationId: int                  // Unique identifier for the organization
    Name: string                             // Name of the organization
    GoverningLaw: string                     // The public international law or agreement governing the organization
    MemberCountries: string list             // List of countries that are members of the organization
    SubordinateBodies: string list option     // Optional list of subordinate bodies governed by the organization
}

type Transaction = {
    Id: string
}

module ConsentChain =
    let startNewTransaction
        (enterprise : Enterprise)
        (dataSubject: DataSubject)
        (consents : Consent list)
        : Transaction = { Id = "todo" }