using Aparta.Authentication.UseCases.UseCases.Account.Common;
using Aparta.Authentication.API.ApiModels.Response;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Aparta.Authentication.API.ApiModels.Account;

namespace Aparta.Authentication.EndToEndTests.Api.Account.UpdateAccount;

[Collection(nameof(UpdateAccountApiTestFixture))]
public class UpdateAccountApiTest
{
    private readonly UpdateAccountApiTestFixture _fixture;

    public UpdateAccountApiTest(UpdateAccountApiTestFixture fixture)
        => _fixture = fixture;

    // It's 12 categories, which generates 12! combinations, which is unfeasible to
    // test all of them. So, I will perform a test to update all at once, then
    // update one at a time, followed by a test with 2 categories, then 3, and so on,
    // up to 12 random categories.

    [Fact(DisplayName = nameof(Should_UpdateAccount))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = _fixture.GetInput();

        var (response, output) = await _fixture
            .ApiClient
            .Put<ApiResponse<AccountModelOutput>>(
                $"/account/{exampleAccount.Id}",
                input
            );
        var dbAccount = await _fixture
            .Persistence.GetById(exampleAccount.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Id.Should().NotBeEmpty();
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
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

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

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_ClientType_PF_And_DocumentNumber))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_ClientType_PF_And_DocumentNumber()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var clientType = Domain.Enum.ClientType.PF;
        var input = new UpdateAccountApiInput(
            clientType: clientType,
            documentNumber: _fixture.GetRandomDocumentNumber(clientType),
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: exampleAccount.TaxRate
        );

        var (response, output) = await _fixture
            .ApiClient
            .Put<ApiResponse<AccountModelOutput>>(
                $"/account/{exampleAccount.Id}",
                input
            );
        var dbAccount = await _fixture
            .Persistence.GetById(exampleAccount.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Id.Should().Be(exampleAccount.Id);
        output.Data.ClientType.Should().Be(input.ClientType);
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(exampleAccount.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(input.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_ClientType_PJ_And_DocumentNumber))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_ClientType_PJ_And_DocumentNumber()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var clientType = Domain.Enum.ClientType.PJ;
        var input = new UpdateAccountApiInput(
            clientType: clientType,
            documentNumber: _fixture.GetRandomDocumentNumber(clientType),
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: exampleAccount.TaxRate
        );

        var (response, output) = await _fixture
            .ApiClient
            .Put<ApiResponse<AccountModelOutput>>(
                $"/account/{exampleAccount.Id}",
                input
            );
        var dbAccount = await _fixture
            .Persistence.GetById(exampleAccount.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Id.Should().Be(exampleAccount.Id);
        output.Data.ClientType.Should().Be(input.ClientType);
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(exampleAccount.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(input.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_Name))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_Name()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: _fixture.GetValidName(),
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: exampleAccount.TaxRate
        );

        var (response, output) = await _fixture
            .ApiClient
            .Put<ApiResponse<AccountModelOutput>>(
                $"/account/{exampleAccount.Id}",
                input
            );
        var dbAccount = await _fixture
            .Persistence.GetById(exampleAccount.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Id.Should().Be(exampleAccount.Id);
        output.Data.ClientType.Should().Be(exampleAccount.ClientType);
        output.Data.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        output.Data.Name.Should().Be(input.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_Address))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_Address()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: _fixture.GetValidAddress(),
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: exampleAccount.TaxRate
        );

        var (response, output) = await _fixture
            .ApiClient
            .Put<ApiResponse<AccountModelOutput>>(
                $"/account/{exampleAccount.Id}",
                input
            );
        var dbAccount = await _fixture
            .Persistence.GetById(exampleAccount.Id);

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Data.Id.Should().Be(exampleAccount.Id);
        output.Data.ClientType.Should().Be(exampleAccount.ClientType);
        output.Data.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        output.Data.Name.Should().Be(exampleAccount.Name);
        output.Data.Address.Should().Be(input.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(input.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_Throw_An_Error_404_When_Not_Found_An_Account_To_Update))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_Throw_An_Error_404_When_Not_Found_An_Account_To_Update()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var randomGuid = Guid.NewGuid();
        var input = _fixture.GetInput();

        var (response, output) = await _fixture
            .ApiClient
            .Put<ProblemDetails>(
                $"/account/{randomGuid}",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
        output.Should().NotBeNull();
        output!.Title.Should().Be("Not Found");
        output.Type.Should().Be("NotFound");
        output.Status.Should().Be((int)StatusCodes.Status404NotFound);
        output.Detail.Should().Be($"Account '{randomGuid}' not found.");
    }

    [Theory(DisplayName = nameof(Should_Throw_An_Error_422_When_Cant_Instantiate_Aggregate))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    [MemberData(
        nameof(UpdateAccountApiTestDataGenerator.GetInvalidInputs),
        parameters: 20,
        MemberType = typeof(UpdateAccountApiTestDataGenerator)
    )]
    public async void Should_Throw_An_Error_422_When_Cant_Instantiate_Aggregate(
        UpdateAccountApiInput input,
        string expectedDetail
    )
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];

        var (response, output) = await _fixture
            .ApiClient
            .Put<ProblemDetails>(
                $"/account/{exampleAccount.Id}",
                input
            );

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status422UnprocessableEntity);
        output.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred");
        output.Type.Should().Be("UnprocessableEntity");
        output.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
        output.Detail.Should().Be(expectedDetail);
    }
}
