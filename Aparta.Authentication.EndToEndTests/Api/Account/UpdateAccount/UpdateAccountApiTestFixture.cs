using Aparta.Authentication.Application.UseCases.Account.UpdateAccount;
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
    public UpdateAccountInput GetInput()
    {
        var clientType = GetRandomClientType();
        return new(
            Guid.NewGuid(),
            clientType,
            GetRandomDocumentNumber(clientType),
            GetValidName(),
            GetValidAddress(),
            GetValidPhone(),
            GetValidBankName(),
            GetValidAgencyNumber(),
            GetValidAccountNumber(),
            GetRandomTaxType(),
            GetValidTaxRate()
        );
    }
}
