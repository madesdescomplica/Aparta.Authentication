using Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Domain.Enum;

using Aparta.Authentication.IntegrationTests.Base;

using Bogus;
using Bogus.Extensions.Brazil;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Infra.Data.EF.UnitOfWork;

[CollectionDefinition(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTestFixtureCollection
    : ICollectionFixture<UnitOfWorkTestFixture>
{ }

public class UnitOfWorkTestFixture : BaseFixture
{ }
