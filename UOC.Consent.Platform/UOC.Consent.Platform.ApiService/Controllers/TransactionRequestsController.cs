using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// TODO
// Transactions should be protected, only if the user is part of the transaction should he see it
[ApiController]
[Route("api/[controller]")]
public class TransactionRequestsController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTransactionRequestById([FromRoute] GetLedgerQuery query) =>
        await mediator
              .Send(query)
              .Match(Ok, _errorMap);

    [HttpGet]
    public async Task<IActionResult> GetTransactionRequestList() =>
        await mediator
              .Send(new GetLedgerQuery(Guid.NewGuid()))
              .Match(Ok, _errorMap);

    [HttpPost]
    public async Task<IActionResult> CreateTransactionRequest([FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTransactionRequest([FromRoute] Guid id, [FromBody] StartLedgerCommand command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTransactionRequest([FromRoute] GetLedgerQuery command) =>
        await mediator
              .Send(command)
              .Match(Ok, _errorMap);
}