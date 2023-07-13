using Aparta.Authentication.Domain.Repository;

using Aparta.Authentication.UseCases.Interfaces;
using Aparta.Authentication.UseCases.Account.Common;

namespace Aparta.Authentication.UseCases.Account.UpdateAccount;

public class UpdateAccount : IUpdateAccount
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccount(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork
    )
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountModelOutput> Handle(
        UpdateAccountInput request,
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepository.Get(
            id: request.Id, 
            cancellationToken: cancellationToken
        );
        account.Update(
            clientType: request.ClientType,
            documentNumber: request.DocumentNumber,
            name: request.Name,
            address: request.Address,
            phone: request.Phone,
            bankCode: request.BankCode,
            bankName: request.BankName,
            agencyNumber: request.AgencyNumber,
            accountNumber: request.AccountNumber,
            taxType: request.TaxType,
            taxRate: request.TaxRate
        );

        await _accountRepository.Update(
            aggregate: account, 
            cancellationToken: cancellationToken
        );
        await _unitOfWork.Commit(cancellationToken);

        return AccountModelOutput.FromAccount(account);
    }
}
