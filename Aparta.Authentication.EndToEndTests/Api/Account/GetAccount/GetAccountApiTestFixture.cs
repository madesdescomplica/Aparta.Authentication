using Aparta.Authentication.EndToEndTests.Api.Account.Common;

using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.GetAccount;

[CollectionDefinition(nameof(GetAccountApiTestFixture))]
public class GetAccountApiTestFixtureCollection
    : ICollectionFixture<GetAccountApiTestFixture>
{ }

public class GetAccountApiTestFixture
    : AccountBaseFixture
{ }
