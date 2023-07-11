﻿using DomainEntity = Aparta.Authentication.Domain.Entity;

using ApplicationUseCase = Aparta.Authentication.Application.UseCases.Account.UpdateAccount;
using Aparta.Authentication.Application.UseCases.Account.UpdateAccount;

using Aparta.Authentication.Infra.Data.EF;
using Aparta.Authentication.Infra.Data.EF.Repositories;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.UpdateAccount;

[Collection(nameof(UpdateAccountTestFixture))]
public class UpdateAccountTest
{
    private readonly UpdateAccountTestFixture _fixture;

    public UpdateAccountTest(UpdateAccountTestFixture fixture)
        => _fixture = fixture;

    [Theory(DisplayName = nameof(Should_UpdateAccount_With_Correct_Values))]
    [Trait("Integration/Application", "UpdateAccount - UseCases")]
    [MemberData(
        nameof(UpdateAccountTestDataGenerator.GetAccountsToUpdate),
        parameters: 5,
        MemberType = typeof(UpdateAccountTestDataGenerator)
    )]
    public async Task Should_UpdateAccount_With_Correct_Values(
        DomainEntity.Account exampleCategory,
        UpdateAccountInput input
    )
    {
        var dbContext = _fixture.CreateDbContext();
        await dbContext.AddRangeAsync(_fixture.GetExampleAccountsList());
        var trackingInfo = await dbContext.AddAsync(exampleCategory);
        dbContext.SaveChanges();
        trackingInfo.State = EntityState.Detached;
        var repository = new AccountRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCase.UpdateAccount(repository, unitOfWork);

        var output = await useCase.Handle(input, CancellationToken.None);
        var dbAccount = await (_fixture.CreateDbContext(true))
            .Accounts
            .FindAsync(output.Id);

        dbAccount.Should().NotBeNull();
        dbAccount!.ClientType.Should().Be(input.ClientType);
        dbAccount.DocumentNumber.Should().Be(input.DocumentNumber);
        dbAccount.Name.Should().Be(input.Name);
        dbAccount.Address.Should().Be(input.Address);
        dbAccount.Phone.Should().Be(input.Phone);
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(input.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.Id.Should().NotBeEmpty();
        dbAccount.CreatedAt.Should().Be(output.CreatedAt);

        output.Should().NotBeNull();
        output!.ClientType.Should().Be(input.ClientType);
        output.DocumentNumber.Should().Be(input.DocumentNumber);
        output.Name.Should().Be(input.Name);
        output.Address.Should().Be(input.Address);
        output.Phone.Should().Be(input.Phone);
        output.BankName.Should().Be(input.BankName);
        output.AgencyNumber.Should().Be(input.AgencyNumber);
        output.AccountNumber.Should().Be(input.AccountNumber);
        output.TaxType.Should().Be(input.TaxType);
        output.TaxRate.Should().Be(input.TaxRate);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBe(default);
    }
}
