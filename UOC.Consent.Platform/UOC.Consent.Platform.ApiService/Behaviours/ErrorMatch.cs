using Microsoft.AspNetCore.Mvc;
using UOC.Consent.Platform.Shared;

namespace UOC.Consent.Platform.ApiService.Behaviours;

public static class ErrorBehaviour
{
    public static IActionResult Map(DomainError error) =>
        error.Match<IActionResult>(
            err => new NotFoundObjectResult(err),
            err => new BadRequestObjectResult(err),
            err => new ObjectResult(err)
        );
}