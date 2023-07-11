using Aparta.Authentication.Application.UseCases.Account.Common;

using Aparta.Authentication.API.ApiModels.Response;

using FluentAssertions;
using System.Net;
using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.CreateAccount;

[Collection(nameof(CreateAccountApiTestFixture))]
public class CreateAccountApiTest
    : IDisposable
{
    private readonly CreateAccountApiTestFixture _fixture;

    public CreateAccountApiTest(CreateAccountApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateAccount))]
    [Trait("EndToEnd/API", "Account/Create - Endpoints")]
    public async Task CreateAccount()
    {
        var input = _fixture.GetInput();

        var (response, output) = await _fixture.
            ApiClient.Post<ApiResponse<AccountModelOutput>>(
                "/Account",
                input
            );
        var dbCategory = await _fixture
            .Persistence
            .GetById(output!.Data.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Created);

        output.Should().NotBeNull();
        output.Data.Should().NotBeNull();
        output.Data.Id.Should().NotBeEmpty();
        output.Data.ClientType.Should().Be(input.ClientType);
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(input.Name);
        output.Data.Address.Should().Be(input.Address);
        output.Data.Phone.Should().Be(input.Phone);
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(input.AgencyNumber);
        output.Data.AccountNumber.Should().Be(input.AccountNumber);
        output.Data.TaxType.Should().Be(input.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should()
            .NotBeSameDateAs(default);

        dbCategory.Should().NotBeNull();
        dbCategory!.Id.Should().NotBeEmpty();
        dbCategory.ClientType.Should().Be(input.ClientType);
        dbCategory.DocumentNumber.Should().Be(input.DocumentNumber);
        dbCategory.Name.Should().Be(input.Name);
        dbCategory.Address.Should().Be(input.Address);
        dbCategory.Phone.Should().Be(input.Phone);
        dbCategory.BankName.Should().Be(input.BankName);
        dbCategory.AgencyNumber.Should().Be(input.AgencyNumber);
        dbCategory.AccountNumber.Should().Be(input.AccountNumber);
        dbCategory.TaxType.Should().Be(input.TaxType);
        dbCategory.TaxRate.Should().Be(input.TaxRate);
        dbCategory.CreatedAt.Should()
            .NotBeSameDateAs(default);
    }

    public void Dispose() => _fixture.CleanPersistence();
}
