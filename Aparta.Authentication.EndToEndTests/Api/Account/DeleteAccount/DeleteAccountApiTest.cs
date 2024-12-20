﻿using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    [Fact(DisplayName = nameof(Should_Throw_An_Error_404_When_Not_Found_Id_To_Delete))]
    [Trait("EndToEnd/API", "Account/Delete - Endpoints")]
    public async void Should_Throw_An_Error_404_When_Not_Found_Id_To_Delete()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var randomGuid = Guid.NewGuid();

        var (response, output) = await _fixture
            .ApiClient
            .Delete<ProblemDetails>(
                $"/account/{randomGuid}"
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
        output.Should().NotBeNull();
        output!.Title.Should().Be("Not Found");
        output.Type.Should().Be("NotFound");
        output.Status.Should().Be(StatusCodes.Status404NotFound);
        output.Detail.Should().Be($"Account '{randomGuid}' not found.");
    }
}
