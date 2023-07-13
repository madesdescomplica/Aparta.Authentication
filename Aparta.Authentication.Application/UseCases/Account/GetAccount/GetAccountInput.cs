using Aparta.Authentication.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.Account.GetAccount;

public class GetAccountInput : IRequest<AccountModelOutput>
{
    public Guid Id { get; set; }
    public GetAccountInput(Guid id)
        => Id = id;
}
