using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;


// TODO

[ApiController]
[Route("api/[controller]")]
public class EnterpriseServicesController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetServiceById(Guid id) =>
        await mediator
              .Send(new GetLedgerQuery(id))
              .Match(Ok, _errorMap);

    [HttpGet]
    public async Task<IActionResult> GetServiceList() =>
        await mediator
              .Send(new GetLedgerQuery(Guid.NewGuid()))
              .Match(Ok, _errorMap);

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);
    
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateService(Guid id, [FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteService([FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);

}