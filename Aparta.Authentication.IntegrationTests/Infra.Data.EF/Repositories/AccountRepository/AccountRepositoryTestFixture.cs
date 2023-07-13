using Aparta.Authentication.IntegrationTests.Base;

using Xunit;

namespace Aparta.Authentication.IntegrationTests.Infra.Data.EF.Repositories.AccountRepository;

[CollectionDefinition(nameof(AccountRepositoryTestFixture))]
public class AccountRepositoryTestFixtureCollection
    : ICollectionFixture<AccountRepositoryTestFixture>
{ }

public class AccountRepositoryTestFixture
    : BaseFixture
{ }
