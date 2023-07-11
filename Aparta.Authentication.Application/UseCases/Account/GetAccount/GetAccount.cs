using Aparta.Authentication.Application.UseCases.Account.Common;
using Aparta.Authentication.Domain.Repository;

namespace Aparta.Authentication.Application.UseCases.Account.GetAccount;

public class GetAccount : IGetAccount
{
    private readonly IAccountRepository _accountRepository;

    public GetAccount(IAccountRepository accountRepository)
        => _accountRepository = accountRepository;

    public async Task<AccountModelOutput> Handle(
        GetAccountInput request,
        CancellationToken cancellationToken
    )
    {
        var account = await _accountRepository.Get(
            request.Id,
            cancellationToken
        );
        return AccountModelOutput.FromAccount(account);
    }
}
