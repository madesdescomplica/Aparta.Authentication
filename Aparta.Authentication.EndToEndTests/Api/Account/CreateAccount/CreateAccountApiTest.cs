using Aparta.Authentication.API.ApiModels.Response;
using Aparta.Authentication.Application.UseCases.Account.Common;

using FluentAssertions;
using System.Net;
using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.CreateAccount;

[Collection(nameof(CreateAccountApiTestFixture))]
public class CreateAccountApiTest
{
    private readonly CreateAccountApiTestFixture _fixture;

    public CreateAccountApiTest(CreateAccountApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_CreateAccount))]
    [Trait("EndToEnd/API", "Account - Endpoints")]
    public async Task Should_CreateAccount()
    {
        // Arrange
        var input = _fixture.GetExampleInput();

        // Act
        var (response, output) = await _fixture
            .ApiClient
            .Post<AccountModelOutput>(
                "/account",
                input
            );
        var dbAccount = await _fixture
            .Persistence
            .GetById(output!.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        dbAccount.Should().NotBeNull();
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.ClientType.Should().Be(input.ClientType);
        output.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Name.Should().Be(input.Name);
        output.Address.Should().Be(input.Address);
        output.Phone.Should().Be(input.Phone);
        output.BankName.Should().Be(input.BankName);
        output.AgencyNumber.Should().Be(input.AgencyNumber);
        output.AccountNumber.Should().Be(input.AccountNumber);
        output.TaxType.Should().Be(input.TaxType);
        output.TaxRate.Should().Be(input.TaxRate);
        output.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(input.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(input.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(input.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }
}
