using MediatR;

namespace Aparta.Authentication.UseCases.UseCases.Account.DeleteAccount;

public interface IDeleteAccount
    : IRequestHandler<DeleteAccountInput>
{ }
