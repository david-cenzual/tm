using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.EnterpriseAggregate;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// TODO

[ApiController]
[Route("api/[controller]")]
public class EnterprisesController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEnterpriseById(int id)
    {
        return await mediator
                     .Send(new GetEnterpriseByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetEnterpriseList()
    {
        return await mediator
                     .Send(new GetEnterprisesListQuery())
                     .Match(Ok, _errorMap);
    }

    [HttpPost]
    [Produces<Enterprises>]
    public async Task<IActionResult> CreateEnterprise([FromBody] Enterprises enterprise)
    {
        return await mediator
                     .Send(new CreateEnterpriseCommand(enterprise))
                     .Match(Ok, _errorMap);
    }


    [HttpPut("{id:int}")]
    [Produces<Enterprises>]
    public async Task<IActionResult> UpdateEnterprise([FromRoute] int id, [FromBody] Enterprises enterprise)
    {
        return await mediator
                     .Send(new UpdateEnterpriseCommand(id, enterprise))
                     .Match(Ok, _errorMap);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEnterprise([FromBody] StartLedgerCommand command)
    {
        return await mediator
                     .Send(command)
                     .Match(Ok, _errorMap);
    }
}