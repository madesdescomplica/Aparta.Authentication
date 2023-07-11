using Aparta.Authentication.Application.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.Application.UseCases.Account.UpdateAccount;

public interface IUpdateAccount
    : IRequestHandler<UpdateAccountInput, AccountModelOutput>
{ }
