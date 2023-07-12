using Aparta.Authentication.Application.UseCases.Account.Common;
using Aparta.Authentication.API.ApiModels.Response;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.GetAccount;

[Collection(nameof(GetAccountApiTestFixture))]
public class GetAccountApiTest
{
    private readonly GetAccountApiTestFixture _fixture;

    public GetAccountApiTest(GetAccountApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_GetAccount))]
    [Trait("EndToEnd/API", "Account/Get - Endpoints")]
    public async Task Should_GetAccount()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];

        var (response, output) = await _fixture
            .ApiClient
            .Get<ApiResponse<AccountModelOutput>>(
                $"/account/{exampleAccount.Id}"
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Id.Should().Be(exampleAccount.Id);
        output.Data.ClientType.Should().Be(exampleAccount.ClientType);
        output.Data.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        output.Data.Name.Should().Be(exampleAccount.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);
    }
}
