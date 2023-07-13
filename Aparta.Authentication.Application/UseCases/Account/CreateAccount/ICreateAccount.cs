using Aparta.Authentication.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.Account.CreateAccount;

public interface ICreateAccount
    : IRequestHandler<CreateAccountInput, AccountModelOutput>
{}
