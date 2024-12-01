using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.PlatformServiceAggregate;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// TODO

[ApiController]
[Route("api/[controller]")]
public class PlatformServicesController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPlatformServiceById(int id)
    {
        return await mediator
                     .Send(new GetPlatformServiceByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetPlatformServiceList()
    {
        return await mediator
                     .Send(new GetPlatformServicesListQuery())
                     .Match(Ok, _errorMap);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPlatformServiceTransactionsList()
    {
        return await mediator
                     .Send(new GetPlatformServicesListQuery())
                     .Match(Ok, _errorMap);
    }


    [HttpPost]
    [Produces<PlatformServices>]
    public async Task<IActionResult> CreatePlatformService([FromBody] PlatformServices platformService)
    {
        return await mediator
                     .Send(new CreatePlatformServiceCommand(platformService))
                     .Match(Ok, _errorMap);
    }


    [HttpPut("{id:int}")]
    [Produces<PlatformServices>]
    public async Task<IActionResult> UpdatePlatformService([FromRoute] int id, [FromBody] PlatformServices service)
    {
        return await mediator
                     .Send(new UpdatePlatformServiceCommand(id, service))
                     .Match(Ok, _errorMap);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlatformService([FromBody] StartLedgerCommand command)
    {
        return await mediator
                     .Send(command)
                     .Match(Ok, _errorMap);
    }
}