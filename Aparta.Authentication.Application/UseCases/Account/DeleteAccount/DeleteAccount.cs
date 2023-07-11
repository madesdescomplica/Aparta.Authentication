using Aparta.Authentication.Application.Interfaces;
using Aparta.Authentication.Domain.Repository;
using MediatR;

namespace Aparta.Authentication.Application.UseCases.Account.DeleteAccount;

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
            request.Id,
            cancellationToken
        );
        await _accountRepository.Delete(account, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
