using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.ConsentAggregate;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// TODO

[ApiController]
[Route("api/[controller]")]
public class ConsentsController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetConsentById(int id)
    {
        return await mediator
                     .Send(new GetConsentByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetConsentList()
    {
        return await mediator
                     .Send(new GetConsentsListQuery())
                     .Match(Ok, _errorMap);
    }

    [HttpPost]
    [Produces<Consents>]
    public async Task<IActionResult> CreateConsent([FromBody] Consents consent)
    {
        return await mediator
                     .Send(new CreateConsentCommand(consent))
                     .Match(Ok, _errorMap);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteConsent([FromBody] StartLedgerCommand command)
    {
        return await mediator
                     .Send(command)
                     .Match(Ok, _errorMap);
    }
}