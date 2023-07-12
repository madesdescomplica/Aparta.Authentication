using DomainEntity = Aparta.Authentication.Domain.Entity;

using Aparta.Authentication.Application.Exceptions;
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
        var useCase = new ApplicationUseCase.UpdateAccount(
            repository, 
            unitOfWork
        );

        var output = await useCase.Handle(input, CancellationToken.None);
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
        dbAccount.BankName.Should().Be(input.BankName);
        dbAccount.AgencyNumber.Should().Be(input.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(input.AccountNumber);
        dbAccount.TaxType.Should().Be(input.TaxType);
        dbAccount.TaxRate.Should().Be(input.TaxRate);
        dbAccount.CreatedAt.Should().Be(output.CreatedAt);

        output.Should().NotBeNull();
        output!.Id.Should().NotBeEmpty();
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
        output.CreatedAt.Should().NotBe(default);
    }

    [Fact(DisplayName = nameof(Should_Throw_NotFoundException_When_Account_Doesnt_Exist))]
    [Trait("Integration/Application", "UpdateAccount - UseCases")]
    public async Task Should_Throw_NotFoundException_When_Account_Doesnt_Exist()
    {
        var randomGuid = Guid.NewGuid();
        var validInput = _fixture.GetValidInput(randomGuid);
        var dbContext = _fixture.CreateDbContext(true);
        var repository = new AccountRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCase.UpdateAccount(
            repository, 
            unitOfWork
        );
        var input = new UpdateAccountInput(
            validInput.Id,
            validInput.ClientType,
            validInput.DocumentNumber,
            validInput.Name,
            validInput.Address,
            validInput.Phone,
            validInput.BankName,
            validInput.AgencyNumber,
            validInput.AccountNumber,
            validInput.TaxType,
            validInput.TaxRate
        );

        var action = async () => await useCase.Handle(
            input, 
            CancellationToken.None
        );

        await action.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Account '{randomGuid}' not found.");
    }
}

