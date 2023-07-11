﻿using Aparta.Authentication.Application.UseCases.Account.UpdateAccount;

using Aparta.Authentication.IntegrationTests.Application.Common;

using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.UpdateAccount;

[CollectionDefinition(nameof(UpdateAccountTestFixture))]
public class UpdateAccountTestFixtureCollection
    : ICollectionFixture<UpdateAccountTestFixture>
{ }

public class UpdateAccountTestFixture
    : AccountUseCasesBaseFixture
{
    public UpdateAccountInput GetValidInput(Guid? id = null)
    {
        var clientType = GetRandomClientType();
        return new UpdateAccountInput(
            id ?? Guid.NewGuid(),
            clientType,
            GetRandomDocumentNumber(clientType),
            GetValidCategoryName(),
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