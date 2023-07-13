using Aparta.Authentication.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.Account.UpdateAccount;

public interface IUpdateAccount
    : IRequestHandler<UpdateAccountInput, AccountModelOutput>
{ }
