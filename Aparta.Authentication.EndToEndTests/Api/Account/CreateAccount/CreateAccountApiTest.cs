﻿using Aparta.Authentication.Application.UseCases.Account.Common;
using Aparta.Authentication.Application.UseCases.Account.CreateAccount;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Trait("EndToEnd/API", "Account/Create - Endpoints")]
    public async Task Should_CreateAccount()
    {
        var input = _fixture.GetInput();

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

        dbAccount.Should().NotBeNull();
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

    [Theory(DisplayName = nameof(Should_Throw_An_Error_400_When_Cant_Instantiate_Aggregate))]
    [Trait("EndToEnd/API", "Account/Create - Endpoints")]
    [MemberData(
        nameof(CreateAccountApiTestDataGenerator.GetInvalidInputsNull),
        parameters: 12,
        MemberType = typeof(CreateAccountApiTestDataGenerator)
    )]
    public async Task Should_Throw_An_Error_400_When_Cant_Instantiate_Aggregate(
        CreateAccountInput input,
        string expectedDetail
    )
    {
        var (response, output) = await _fixture.
            ApiClient.Post<ProblemDetails>(
                "/account",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred.");
        output.Type.Should().Be("https://tools.ietf.org/html/rfc7231#section-6.5.1");
        output.Status.Should().Be((int)StatusCodes.Status400BadRequest);
        (expectedDetail != null && output.Detail == null).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Should_Throw_An_Error_422_When_Cant_Instantiate_Aggregate))]
    [Trait("EndToEnd/API", "Account/Create - Endpoints")]
    [MemberData(
        nameof(CreateAccountApiTestDataGenerator.GetInvalidInputs),
        parameters: 20,
        MemberType = typeof(CreateAccountApiTestDataGenerator)
    )]
    public async Task Should_Throw_An_Error_422_When_Cant_Instantiate_Aggregate(
        CreateAccountInput input,
        string expectedDetail
    )
    {
        var (response, output) = await _fixture.
            ApiClient.Post<ProblemDetails>(
                "/account",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        output.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred");
        output.Type.Should().Be("UnprocessableEntity");
        output.Status.Should().Be((int)StatusCodes.Status422UnprocessableEntity);
        output.Detail.Should().Be(expectedDetail);
    }
}
