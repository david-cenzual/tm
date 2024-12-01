using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.ElectronicIdAggregate;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// TODO

[ApiController]
[Route("api/[controller]")]
public class ElectronicIdsController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetElectronicIdById(int id)
    {
        return await mediator
                     .Send(new GetElectronicIdByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetElectronicIdList()
    {
        return await mediator
                     .Send(new GetElectronicIdsListQuery())
                     .Match(Ok, _errorMap);
    }

    [HttpPost]
    [Produces<ElectronicIds>]
    public async Task<IActionResult> CreateElectronicId([FromBody] ElectronicIds electronicId)
    {
        return await mediator
                     .Send(new CreateElectronicIdCommand(electronicId))
                     .Match(Ok, _errorMap);
    }


    [HttpPut("{id:int}")]
    [Produces<ElectronicIds>]
    public async Task<IActionResult> UpdateElectronicId([FromRoute] int id, [FromBody] ElectronicIds electronicId)
    {
        return await mediator
                     .Send(new UpdateElectronicIdCommand(id, electronicId))
                     .Match(Ok, _errorMap);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteElectronicId([FromBody] StartLedgerCommand command)
    {
        return await mediator
                     .Send(command)
                     .Match(Ok, _errorMap);
    }
}