using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.TransactionRequestAggregate;
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTransactionRequestById(int id)
    {
        return await mediator
                     .Send(new GetTransactionByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionRequestList()
    {
        return await mediator
                     .Send(new GetTransactionRequestsListQuery())
                     .Match(Ok, _errorMap);
    }

    [HttpPost]
    [Produces<TransactionRequests>]
    public async Task<IActionResult> CreateTransactionRequest([FromBody] TransactionRequests transactionRequest)
    {
        return await mediator
                     .Send(new CreateTransactionRequestCommand(transactionRequest))
                     .Match(Ok, _errorMap);
    }

    [HttpPut("{id:int}")]
    [Produces<TransactionRequests>]
    public async Task<IActionResult> UpdateTransactionRequest([FromRoute] int id, [FromBody] TransactionRequests transactionRequest)
    {
        return await mediator
                     .Send(new UpdateTransactionRequestCommand(id, transactionRequest))
                     .Match(Ok, _errorMap);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTransactionRequest([FromRoute] StartLedgerCommand command)
    {
        return await mediator
                     .Send(command)
                     .Match(Ok, _errorMap);
    }
}