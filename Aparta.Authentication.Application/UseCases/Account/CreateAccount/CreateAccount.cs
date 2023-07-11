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
        CreateAccountInput request,
        CancellationToken cancellationToken
    )
    {
        var account = new Domain.Entity.Account(
            clientType: request.ClientType,
            documentNumber: request.DocumentNumber,
            name: request.Name,
            address: request.Address,
            phone: request.Phone,
            bankName: request.BankName,
            agencyNumber: request.AgencyNumber,
            accountNumber: request.AccountNumber,
            taxType: request.TaxType,
            taxRate: request.TaxRate
        );

        await _accountRepository.Insert(
            account,
            cancellationToken
        );
        await _unitOfWork.Commit(cancellationToken);

        return AccountModelOutput.FromAccount(account);
    }
}

