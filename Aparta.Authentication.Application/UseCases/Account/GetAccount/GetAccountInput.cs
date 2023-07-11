using Aparta.Authentication.Application.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.Application.UseCases.Account.GetAccount;

public class GetAccountInput : IRequest<AccountModelOutput>
{
    public Guid Id { get; set; }
    public GetAccountInput(Guid id)
        => Id = id;
}
