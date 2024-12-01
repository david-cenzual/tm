using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.DataSubjectAggregate;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// TODO

[ApiController]
[Route("api/[controller]")]
public class DataSubjectsController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDataSubjectById(int id)
    {
        return await mediator
                     .Send(new GetDataSubjectByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetDataSubjectList()
    {
        return await mediator
                     .Send(new GetDataSubjectsListQuery())
                     .Match(Ok, _errorMap);
    }

    [HttpPost]
    [Produces<DataSubjects>]
    public async Task<IActionResult> CreateDataSubject([FromBody] DataSubjects dataSubject)
    {
        return await mediator
                     .Send(new CreateDataSubjectCommand(dataSubject))
                     .Match(x => Created($"DataSubjects/{x.Reference}", x), _errorMap);
    } 
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDataSubject([FromBody] StartLedgerCommand command)
    {
        return await mediator
                     .Send(command)
                     .Match(Ok, _errorMap);
    }
}