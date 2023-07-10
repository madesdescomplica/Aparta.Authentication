using Aparta.Authentication.Application.UseCases.Account.CreateAccount;

using Aparta.Authentication.IntegrationTests.Application.Common;

using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.CreateAccount;

[CollectionDefinition(nameof(CreateAccountTestFixture))]
public class CreateAccountTestFixtureCollection
    : ICollectionFixture<CreateAccountTestFixture>
{ }

public class CreateAccountTestFixture
    : AccountUseCasesBaseFixture
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