using MediatR;

namespace Aparta.Authentication.Application.UseCases.Account.DeleteAccount;

public interface IDeleteAccount
    : IRequestHandler<DeleteAccountInput>
{ }
