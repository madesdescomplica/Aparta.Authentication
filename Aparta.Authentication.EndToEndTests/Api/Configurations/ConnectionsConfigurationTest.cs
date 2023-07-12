using Aparta.Authentication.API.Configurations;
using Aparta.Authentication.Infra.Data.EF;

using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Configurations;

public class ConnectionsConfigurationTests
{
    [Fact(DisplayName = nameof(Should_AddAppConnection_Register_DbContext))]
    [Trait("EndToEnd/API", "DBConnections - Endpoints")]
    public void Should_AddAppConnection_Register_DbContext()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAppConnection();
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetService<ApartaAuthenticationDbContext>();

        // Assert
        Assert.NotNull(dbContext);
        Assert.NotNull(dbContext!.Database.ProviderName);
    }
}