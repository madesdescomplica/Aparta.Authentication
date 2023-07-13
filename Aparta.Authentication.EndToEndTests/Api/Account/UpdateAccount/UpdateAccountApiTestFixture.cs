using Aparta.Authentication.API.ApiModels.Account;
using Aparta.Authentication.EndToEndTests.Api.Account.Common;

using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.UpdateAccount;

[CollectionDefinition(nameof(UpdateAccountApiTestFixture))]
public class UpdateAccountApiTestFixtureCollection
    : ICollectionFixture<UpdateAccountApiTestFixture>
{ }


public class UpdateAccountApiTestFixture
    : AccountBaseFixture
{
    public UpdateAccountApiInput GetInput()
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
