using Aparta.Authentication.UseCases.Account.CreateAccount;

using Aparta.Authentication.EndToEndTests.Api.Account.Common;

using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.CreateAccount;

[CollectionDefinition(nameof(CreateAccountApiTestFixture))]
public class CreateAccountApiTestFixtureCollection
    : ICollectionFixture<CreateAccountApiTestFixture>
{ }

public class CreateAccountApiTestFixture
    : AccountBaseFixture
{
    public CreateAccountInput GetInput()
    {
        var clientType = GetRandomClientType();
        return new(
            clientType,
            GetRandomDocumentNumber(clientType),
            GetValidName(),
            GetValidAddress(),
            GetValidPhone(),
            GetValidBankCode(),
            GetValidBankName(),
            GetValidAgencyNumber(),
            GetValidAccountNumber(),
            GetRandomTaxType(),
            GetValidTaxRate()
        );
    }
}
