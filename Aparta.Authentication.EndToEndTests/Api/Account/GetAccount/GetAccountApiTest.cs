using Aparta.Authentication.Application.UseCases.Account.Common;
using Aparta.Authentication.API.ApiModels.Response;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.Mvc;

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

    [Fact(DisplayName = nameof(Should_Throw_An_Error_404_When_Not_Found))]
    [Trait("EndToEnd/API", "Account/Get - Endpoints")]
    public async Task Should_Throw_An_Error_404_When_Not_Found()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var randomGuid = Guid.NewGuid();

        var (response, output) = await _fixture
            .ApiClient
            .Get<ProblemDetails>(
                $"/account/{randomGuid}"
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
        output.Should().NotBeNull();
        output!.Status.Should().Be((int)StatusCodes.Status404NotFound);
        output.Type.Should().Be("NotFound");
        output.Title.Should().Be("Not Found");
        output.Detail.Should().Be($"Account '{randomGuid}' not found.");
    }
}
