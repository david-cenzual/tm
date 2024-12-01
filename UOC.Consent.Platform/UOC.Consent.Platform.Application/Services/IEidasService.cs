using UOC.Consent.Platform.Domain.ElectronicIdAggregate;

namespace UOC.Consent.Platform.Application.Services;

public interface IEidasService
{
    public Task<ElectronicIds> GetEidasIdentifierAsync(string hash);
}