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
public class EnterprisesController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEnterpriseById(Guid id) =>
        await mediator
              .Send(new GetLedgerQuery(id))
              .Match(Ok, _errorMap);

    [HttpGet]
    public async Task<IActionResult> GetEnterpriseList() =>
        await mediator
              .Send(new GetLedgerQuery(Guid.NewGuid()))
              .Match(Ok, _errorMap);

    [HttpPost]
    public async Task<IActionResult> CreateEnterprise([FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);
    
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEnterprise(Guid id, [FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEnterprise([FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);

}