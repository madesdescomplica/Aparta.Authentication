using MediatR;

namespace Aparta.Authentication.UseCases.Account.DeleteAccount;

public interface IDeleteAccount
    : IRequestHandler<DeleteAccountInput>
{ }
