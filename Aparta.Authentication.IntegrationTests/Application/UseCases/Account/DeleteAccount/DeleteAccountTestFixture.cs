using Aparta.Authentication.IntegrationTests.Application.Common;

using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.DeleteAccount;

[CollectionDefinition(nameof(DeleteAccountTestFixture))]
public class DeleteAccountTestFixtureCollection
    : ICollectionFixture<DeleteAccountTestFixture>
{ }

public class DeleteAccountTestFixture
    : AccountUseCasesBaseFixture
{ }
