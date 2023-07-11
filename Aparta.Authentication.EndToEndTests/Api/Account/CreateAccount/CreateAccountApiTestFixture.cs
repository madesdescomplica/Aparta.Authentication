using Aparta.Authentication.Application.UseCases.Account.CreateAccount;

using Aparta.Authentication.EndToEndTests.Api.Account.Common;

namespace Aparta.Authentication.EndToEndTests.Api.CreateAccount;

public class CreateAccountApiTestFixture
    : AccountBaseFixture
{
    public CreateAccountInput GetInput()
    {
        var account = GetValidAccount(GetRandomClientType());
        return new CreateAccountInput(
            account.ClientType,
            account.DocumentNumber,
            account.Name,
            account.Address,
            account.Phone,
            account.BankName,
            account.AgencyNumber,
            account.AccountNumber,
            account.TaxType,
            account.TaxRate
        );
    }
}
