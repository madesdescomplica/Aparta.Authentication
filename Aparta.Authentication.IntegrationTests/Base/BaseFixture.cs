using Aparta.Authentication.Infra.Data.EF;

using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.IntegrationTests.Base;

public class BaseFixture
{
    protected Faker Faker { get; set; }

    public BaseFixture()
        => Faker = new Faker("pt_BR");

    public ApartaAuthenticationDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new ApartaAuthenticationDbContext(
            new DbContextOptionsBuilder<ApartaAuthenticationDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );

        if (!preserveData)
            context.Database.EnsureDeleted();

        return context;
    }
}