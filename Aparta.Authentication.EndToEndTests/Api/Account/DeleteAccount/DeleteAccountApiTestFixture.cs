using Aparta.Authentication.EndToEndTests.Api.Account.Common;

using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.DeleteAccount;

[CollectionDefinition(nameof(DeleteAccountApiTestFixture))]
public class DeleteAccountApiTestFixtureCollection
    : ICollectionFixture<DeleteAccountApiTestFixture>
{ }

public class DeleteAccountApiTestFixture
    : AccountBaseFixture
{ }
