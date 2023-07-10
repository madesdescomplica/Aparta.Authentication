using Aparta.Authentication.Infra.Data.EF;

using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.IntegrationTests.Base;

public class BaseFixture
{
    protected Faker Faker { get; set; }

    public BaseFixture()
        => Faker = new Faker("pt_BR");

    public ApartaAccountDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new ApartaAccountDbContext(
            new DbContextOptionsBuilder<ApartaAccountDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );

        if (!preserveData)
            context.Database.EnsureDeleted();

        return context;
    }
}