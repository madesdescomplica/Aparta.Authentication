using Aparta.Authentication.IntegrationTests.Application.Common;

using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.GetAccount;

[CollectionDefinition(nameof(GetAccountTestFixture))]
public class GetAccountTestFixtureCollection
    : ICollectionFixture<GetAccountTestFixture>
{ }

public class GetAccountTestFixture
    : AccountUseCasesBaseFixture
{ }