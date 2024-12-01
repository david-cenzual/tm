using MediatR;
using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.Application.Queries;
using UOC.Consent.Platform.Shared;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace UOC.Consent.Platform.ApiService.Controllers;

// Ledgers should be protected, only if the user is part of the ledger should he see it
[ApiController]
[Route("api/[controller]")]
public class LedgersController(IMediator mediator) : Controller
{
    private readonly Func<DomainError, IActionResult> _errorMap = ErrorBehaviour.Map;

    [HttpGet]
    public async Task<IActionResult> GetLedger()
    {
        return await mediator
                     .Send(new GetLedgerByIdQuery())
                     .Match(Ok, _errorMap);
    }
}