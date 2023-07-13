using MediatR;

namespace Aparta.Authentication.UseCases.Account.DeleteAccount;

public class DeleteAccountInput : IRequest
{
    public Guid Id { get; set; }
    public DeleteAccountInput(Guid id)
        => Id = id;
}
