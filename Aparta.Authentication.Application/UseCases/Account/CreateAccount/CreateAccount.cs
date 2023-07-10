using Aparta.Authentication.Domain.Repository;

using Aparta.Authentication.Application.Interfaces;
using Aparta.Authentication.Application.UseCases.Account.Common;

namespace Aparta.Authentication.Application.UseCases.Account.CreateAccount;

public class CreateAccount : ICreateAccount
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccount(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork
    )
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountModelOutput> Handle(
        CreateAccountInput input,
        CancellationToken cancellationToken
    )
    {
        var account = new Domain.Entity.Account(
            input.ClientType,
            input.DocumentNumber,
            input.Name,
            input.Address,
            input.Phone,
            input.BankName,
            input.AgencyNumber,
            input.AccountNumber,
            input.TaxType,
            input.TaxRate
        );

        await _accountRepository.Insert(
            account,
            cancellationToken
        );
        await _unitOfWork.Commit(cancellationToken);

        return AccountModelOutput.FromAccount(account);
    }
}

