using Aparta.Authentication.Domain.Repository;
using Aparta.Authentication.Application.UseCases.Account.Common;

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
            id: request.Id,
            cancellationToken: cancellationToken
        );
        return AccountModelOutput.FromAccount(account);
    }
}
