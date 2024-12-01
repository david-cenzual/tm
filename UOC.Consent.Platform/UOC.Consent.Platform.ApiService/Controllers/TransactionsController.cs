using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Commands;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Domain.TransactionAggregate;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        return await mediator
                     .Send(new GetTransactionByIdQuery(id))
                     .Match(Ok, _errorMap);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsList()
    {
        return await mediator
                     .Send(new GetTransactionsListQuery())
                     .Match(Ok, _errorMap);
    }

    [HttpPost]
    [Produces<Transactions>]
    public async Task<IActionResult> CreateTransaction([FromBody] Transactions transaction)
    {
        return await mediator
                     .Send(new CreateTransactionCommand(transaction))
                     .Match(Ok, _errorMap);
    }
}