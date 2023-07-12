using Aparta.Authentication.Domain.Repository;
using Aparta.Authentication.UseCases.Interfaces;

using MediatR;

namespace Aparta.Authentication.UseCases.UseCases.Account.DeleteAccount;

public class DeleteAccount : IDeleteAccount
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccount(
        IAccountRepository accountRepository, 
        IUnitOfWork unitOfWork
    )
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteAccountInput request, 
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepository.Get(
            id: request.Id,
            cancellationToken: cancellationToken
        );
        await _accountRepository.Delete(
            aggregate: account, 
            cancellationToken: cancellationToken
        );
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
