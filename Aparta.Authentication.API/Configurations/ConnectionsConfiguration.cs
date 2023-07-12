using Aparta.Authentication.Infra.Data.EF;

using Microsoft.EntityFrameworkCore;

namespace Aparta.Authentication.API.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnection(
        this IServiceCollection services
     )
    {
        services.AddDbContext<ApartaAuthenticationDbContext>(
            options => options.UseInMemoryDatabase(
                "InMemoryDatabase"
            )
        );

        return services;
    }
}
