using Aparta.Authentication.Application.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.Application.UseCases.Account.CreateAccount;

public interface ICreateAccount
    : IRequestHandler<CreateAccountInput, AccountModelOutput>
{}
