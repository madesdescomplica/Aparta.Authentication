using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.DeleteAccount;

[Collection(nameof(DeleteAccountApiTestFixture))]
public class DeleteAccountApiTest
{
    private readonly DeleteAccountApiTestFixture _fixture;

    public DeleteAccountApiTest(DeleteAccountApiTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_DeleteAccount))]
    [Trait("EndToEnd/API", "Account/Delete - Endpoints")]
    public async void Should_DeleteAccount()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];

        var (response, output) = await _fixture
            .ApiClient
            .Delete<object>(
                $"/account/{exampleAccount.Id}"
            );
        var persistenceAccount = await _fixture.Persistence
            .GetById(exampleAccount.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status204NoContent);
        output.Should().BeNull();
        persistenceAccount.Should().BeNull();
    }
}
