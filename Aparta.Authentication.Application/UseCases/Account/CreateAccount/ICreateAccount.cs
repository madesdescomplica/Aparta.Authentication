using Aparta.Authentication.UseCases.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.UseCases.Account.CreateAccount;

public interface ICreateAccount
    : IRequestHandler<CreateAccountInput, AccountModelOutput>
{}
