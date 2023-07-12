using Aparta.Authentication.UseCases.UseCases.Account.Common;
using Aparta.Authentication.UseCases.UseCases.Account.CreateAccount;
using Aparta.Authentication.UseCases.UseCases.Account.DeleteAccount;
using Aparta.Authentication.UseCases.UseCases.Account.GetAccount;
using Aparta.Authentication.UseCases.UseCases.Account.UpdateAccount;

using Aparta.Authentication.API.ApiModels.Account;
using Aparta.Authentication.API.ApiModels.Response;

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
            new ApiResponse<AccountModelOutput>(output)
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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateAccountApiInput apiInput,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(
            new UpdateAccountInput(
                id: id,
                clientType: apiInput.ClientType,
                documentNumber: apiInput.DocumentNumber,
                name: apiInput.Name,
                address: apiInput.Address,
                phone: apiInput.Phone,
                bankName: apiInput.BankName,
                agencyNumber: apiInput.AgencyNumber,
                accountNumber: apiInput.AccountNumber,
                taxType: apiInput.TaxType,
                taxRate: apiInput.TaxRate
            ),
            cancellationToken
        );
        return Ok(new ApiResponse<AccountModelOutput>(output));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(
            new DeleteAccountInput(id), 
            cancellationToken
        );
        return NoContent();
    }
}
