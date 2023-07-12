using Aparta.Authentication.UseCases.UseCases.Account.Common;

using Aparta.Authentication.API.ApiModels.Account;
using Aparta.Authentication.API.ApiModels.Response;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.UpdateAccount;

[Collection(nameof(UpdateAccountApiTestFixture))]
public class UpdateAccountApiTest
{
    private readonly UpdateAccountApiTestFixture _fixture;

    public UpdateAccountApiTest(UpdateAccountApiTestFixture fixture)
        => _fixture = fixture;

    // It's 10 categories, which generates 10! combinations or (3,628,800 combinations),
    // which is unfeasible to test all of them. So, I will perform a test to update all
    // at once, then update one at a time, followed by a test with 2 categories,
    // then 3, and so on, up to 9 random categories.

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

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_ClientType_And_DocumentNumber))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_ClientType_And_DocumentNumber()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var clientType = _fixture.GetRandomClientType();
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

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_Phone))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_Phone()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: _fixture.GetValidPhone(),
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
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(input.Phone);
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
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_BankName))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_BankName()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: _fixture.GetValidBankName(),
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
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(input.BankName);
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
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_AgencyNumber))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_AgencyNumber()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName:exampleAccount.BankName,
            agencyNumber: _fixture.GetValidAgencyNumber(),
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
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(input.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_AccountNumber))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_AccountNumber()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: _fixture.GetValidAccountNumber(),
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
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(input.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(input.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_TaxType))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_TaxType()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: _fixture.GetRandomTaxType(),
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
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(input.TaxType);
        output.Data.TaxRate.Should().Be(exampleAccount.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(exampleAccount.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_Only_TaxRate))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_Only_TaxRate()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: exampleAccount.Name,
            address: exampleAccount.Address,
            phone: exampleAccount.Phone,
            bankName: exampleAccount.BankName,
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(exampleAccount.Phone);
        output.Data.BankName.Should().Be(exampleAccount.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(exampleAccount.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(exampleAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_2_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_2_Fields()
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
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.TaxRate.Should().Be(input.TaxRate);
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
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_3_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_3_Fields()
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
            bankName: _fixture.GetValidBankName(),
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(exampleAccount.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_4_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_4_Fields()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: exampleAccount.DocumentNumber,
            name: _fixture.GetValidName(),
            address: exampleAccount.Address,
            phone: _fixture.GetValidPhone(),
            bankName: _fixture.GetValidBankName(),
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.Phone.Should().Be(input.Phone);
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(exampleAccount.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_5_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_5_Fields()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: _fixture
                .GetRandomDocumentNumber(exampleAccount.ClientType),
            name: _fixture.GetValidName(),
            address: exampleAccount.Address,
            phone: _fixture.GetValidPhone(),
            bankName: _fixture.GetValidBankName(),
            agencyNumber: exampleAccount.AgencyNumber,
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(input.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(input.Phone);
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(exampleAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }
    
    [Fact(DisplayName = nameof(Should_UpdateAccount_With_6_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_6_Fields()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: _fixture
                .GetRandomDocumentNumber(exampleAccount.ClientType),
            name: _fixture.GetValidName(),
            address: exampleAccount.Address,
            phone: _fixture.GetValidPhone(),
            bankName: _fixture.GetValidBankName(),
            agencyNumber: _fixture.GetValidAgencyNumber(),
            accountNumber: exampleAccount.AccountNumber,
            taxType: exampleAccount.TaxType,
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(input.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(input.Phone);
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(input.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(exampleAccount.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(exampleAccount.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_7_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_7_Fields()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: _fixture
                .GetRandomDocumentNumber(exampleAccount.ClientType),
            name: _fixture.GetValidName(),
            address: exampleAccount.Address,
            phone: _fixture.GetValidPhone(),
            bankName: _fixture.GetValidBankName(),
            agencyNumber: _fixture.GetValidAgencyNumber(),
            accountNumber: exampleAccount.AccountNumber,
            taxType: _fixture.GetRandomTaxType(),
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(input.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(input.Phone);
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(input.AgencyNumber);
        output.Data.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        output.Data.TaxType.Should().Be(input.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(exampleAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_8_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_8_Fields()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: _fixture
                .GetRandomDocumentNumber(exampleAccount.ClientType),
            name: _fixture.GetValidName(),
            address: exampleAccount.Address,
            phone: _fixture.GetValidPhone(),
            bankName: _fixture.GetValidBankName(),
            agencyNumber: _fixture.GetValidAgencyNumber(),
            accountNumber: _fixture.GetValidAccountNumber(),
            taxType: _fixture.GetRandomTaxType(),
            taxRate: _fixture.GetValidTaxRate()
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
        output.Data.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Data.Name.Should().Be(input.Name);
        output.Data.Address.Should().Be(exampleAccount.Address);
        output.Data.Phone.Should().Be(input.Phone);
        output.Data.BankName.Should().Be(input.BankName);
        output.Data.AgencyNumber.Should().Be(input.AgencyNumber);
        output.Data.AccountNumber.Should().Be(input.AccountNumber);
        output.Data.TaxType.Should().Be(input.TaxType);
        output.Data.TaxRate.Should().Be(input.TaxRate);
        output.Data.CreatedAt.Should().NotBeSameDateAs(default);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(exampleAccount.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(input.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(Should_UpdateAccount_With_9_Fields))]
    [Trait("EndToEnd/API", "Account/Update - Endpoints")]
    public async void Should_UpdateAccount_With_9_Fields()
    {
        var exampleAccountsList = _fixture.GetExampleAccountsList(20);
        await _fixture.Persistence.InserList(exampleAccountsList);
        var exampleAccount = exampleAccountsList[10];
        var input = new UpdateAccountApiInput(
            clientType: exampleAccount.ClientType,
            documentNumber: _fixture
                .GetRandomDocumentNumber(exampleAccount.ClientType),
            name: _fixture.GetValidName(),
            address: _fixture.GetValidAddress(),
            phone: _fixture.GetValidPhone(),
            bankName: _fixture.GetValidBankName(),
            agencyNumber: _fixture.GetValidAgencyNumber(),
            accountNumber: _fixture.GetValidAccountNumber(),
            taxType: _fixture.GetRandomTaxType(),
            taxRate: _fixture.GetValidTaxRate()
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
        dbAccount.ClientType.Should().Be(exampleAccount.ClientType);
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
