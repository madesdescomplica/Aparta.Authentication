using Aparta.Authentication.UseCases.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.UseCases.Account.UpdateAccount;

public interface IUpdateAccount
    : IRequestHandler<UpdateAccountInput, AccountModelOutput>
{ }
