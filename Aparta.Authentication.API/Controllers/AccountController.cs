using Aparta.Authentication.API.ApiModels.Response;
using Aparta.Authentication.Application.UseCases.Account.Common;
using Aparta.Authentication.Application.UseCases.Account.CreateAccount;
using Aparta.Authentication.Application.UseCases.Account.GetAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aparta.Authentication.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(AccountModelOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateAccountInput input,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create),
            new { output.Id },
            output
        );
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(
            new GetAccountInput(id), 
            cancellationToken
        );
        return Ok(new ApiResponse<AccountModelOutput>(output));
    }
}
