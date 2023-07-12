using Aparta.Authentication.Infra.Data.EF;

using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.EndToEndTests.Base;

public class BaseFixture
{
    protected Faker Faker { get; set; }

    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }

    public HttpClient HttpClient { get; set; }

    public ApiClient ApiClient { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(HttpClient);
    }

    public ApartaAuthenticationDbContext CreateDbContext()
    {
        var context = new ApartaAuthenticationDbContext(
            new DbContextOptionsBuilder<ApartaAuthenticationDbContext>()
                .UseInMemoryDatabase("end2end-tests-db")
                .Options
        );

        return context;
    }
}
