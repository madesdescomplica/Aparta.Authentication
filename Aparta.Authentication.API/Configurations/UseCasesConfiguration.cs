using Aparta.Authentication.Domain.Repository;

using Aparta.Authentication.UseCases.Interfaces;
using Aparta.Authentication.UseCases.Account.CreateAccount;

using Aparta.Authentication.Infra.Data.EF;
using Aparta.Authentication.Infra.Data.EF.Repositories;

using MediatR;

namespace Aparta.Authentication.API.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(typeof(CreateAccount));
        services.AddRepositories();
        return services;
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
