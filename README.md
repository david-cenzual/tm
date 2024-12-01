### A

Usad esto

Migrar
`dotnet ef migrations add -s .\UOC.Consent.Platform.ApiService\UOC.Consent.Platform.ApiService.csproj -p .\UOC.Consent.Platform.Infrastructure\UOC.Consent.Platform.Infrastructure.csproj InitialCreate`
Update
`dotnet ef database update -s .\UOC.Consent.Platform.ApiService\UOC.Consent.Platform.ApiService.csproj -p .\UOC.Consent.Platform.Infrastructure\UOC.Consent.Platform.Infrastructure.csproj`