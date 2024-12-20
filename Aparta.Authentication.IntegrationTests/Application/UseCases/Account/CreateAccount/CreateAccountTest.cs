﻿using Aparta.Authentication.Domain.Exceptions;

using ApplicationUseCases = Aparta.Authentication.UseCases.Account.CreateAccount;
using Aparta.Authentication.UseCases.Account.CreateAccount;

using Aparta.Authentication.Infra.Data.EF;
using Aparta.Authentication.Infra.Data.EF.Repositories;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.CreateAccount;

[Collection(nameof(CreateAccountTestFixture))]
public class CreateAccountTest
{
    private readonly CreateAccountTestFixture _fixture;

    public CreateAccountTest(CreateAccountTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_CreateAccount_With_Correct_Values))]
    [Trait("Integration/Application", "CreateAccount - UseCases")]
    public async void Should_CreateAccount_With_Correct_Values()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new AccountRepository(dbContext);
        var unitOfWorkMock = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.CreateAccount(
            repository,
            unitOfWorkMock
        );
        var input = _fixture.GetInput();
        var output = await useCase.Handle(
            input,
            CancellationToken.None
        );

        var dbAccount = await (_fixture.CreateDbContext(true))
            .Accounts
            .FindAsync(output.Id);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(input.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(input.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankCode.Should().Be(input.BankCode);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(input.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().Be(output.CreatedAt);

        output.Should().NotBeNull();
        output!.ClientType.Should().Be(input.ClientType);
        output.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Name.Should().Be(input.Name);
        output.Address.Should().Be(input.Address);
        output.Phone.Should().Be(input.Phone);
        output.BankCode.Should().Be(input.BankCode);
        output.BankName.Should().Be(input.BankName);
        output.AgencyNumber.Should().Be(input.AgencyNumber);
        output.AccountNumber.Should().Be(input.AccountNumber);
        output.TaxType.Should().Be(input.TaxType);
        output.TaxRate.Should().Be(input.TaxRate);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBe(default);
    }

    [Theory(DisplayName = nameof(Should_Throw_When_Cant_Instantiate_Account))]
    [Trait("Integration/Application", "CreateAccount - UseCases")]
    [MemberData(
        nameof(CreateAccountTestDataGenerator.GetInvalidInputs),
        parameters: 36,
        MemberType = typeof(CreateAccountTestDataGenerator)
    )]
    public async void Should_Throw_When_Cant_Instantiate_Account(
        CreateAccountInput input,
        string expectedExceptionMessage
    )
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new AccountRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.CreateAccount(
            repository, 
            unitOfWork
        );

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);
        var dbCategoriesList = _fixture.CreateDbContext(true)
            .Accounts
            .AsNoTracking()
            .ToList();

        await task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(expectedExceptionMessage);
        dbCategoriesList.Should().HaveCount(0);
    }
}

