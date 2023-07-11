using Aparta.Authentication.Application.Exceptions;

using Aparta.Authentication.Infra.Data.EF;
using Repository = Aparta.Authentication.Infra.Data.EF.Repositories;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Infra.Data.EF.Repositories.AccountRepository;

[Collection(nameof(AccountRepositoryTestFixture))]
public class AccountRepositoryTest
{
    private readonly AccountRepositoryTestFixture _fixture;

    public AccountRepositoryTest(AccountRepositoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_Call_Insert_With_Correct_Values))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Call_Insert_With_Correct_Values()
    {
        var clientType = _fixture.GetRandomClientType();
        var validAccount = _fixture.GetValidAccount(clientType);
        var dbContext = _fixture.CreateDbContext();
        var accountRepository = new Repository.AccountRepository(dbContext);

        await accountRepository.Insert(validAccount, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var dbAccount = await (_fixture.CreateDbContext(true))
            .Accounts.FindAsync(validAccount.Id);

        dbAccount.Should().NotBeNull();
        dbAccount!.ClientType.Should().Be(validAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(validAccount.DocumentNumber);
        dbAccount.Name.Should().Be(validAccount.Name);
        dbAccount.Address.Should().Be(validAccount.Address);
        dbAccount.Phone.Should().Be(validAccount.Phone);
        dbAccount.BankName.Should().Be(validAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(validAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(validAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(validAccount.TaxType);
        dbAccount.TaxRate.Should().Be(validAccount.TaxRate);
        dbAccount.Id.Should().NotBeEmpty();
        dbAccount.CreatedAt.Should().Be(validAccount.CreatedAt);
    }
    
    [Fact(DisplayName = nameof(Should_Call_Get_With_Correct_Values))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Call_Get_With_Correct_Values()
    {
        var clientType = _fixture.GetRandomClientType();
        var validAccount = _fixture.GetValidAccount(clientType);
        var exampleAccountList = _fixture.GetExampleAccountsList(15);
        exampleAccountList.Add(validAccount);
        var dbContext = _fixture.CreateDbContext();
        
        await dbContext.AddRangeAsync(exampleAccountList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var accountRepository = new Repository.AccountRepository(
            _fixture.CreateDbContext(true)
        );
        var dbAccount = await accountRepository.Get(
            validAccount.Id,
            CancellationToken.None
        );

        dbAccount.Should().NotBeNull();
        dbAccount!.ClientType.Should().Be(validAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(validAccount.DocumentNumber);
        dbAccount.Name.Should().Be(validAccount.Name);
        dbAccount.Address.Should().Be(validAccount.Address);
        dbAccount.Phone.Should().Be(validAccount.Phone);
        dbAccount.BankName.Should().Be(validAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(validAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(validAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(validAccount.TaxType);
        dbAccount.TaxRate.Should().Be(validAccount.TaxRate);
        dbAccount.Id.Should().NotBeEmpty();
        dbAccount.CreatedAt.Should().Be(validAccount.CreatedAt);
    }

    [Fact(DisplayName = nameof(Should_Throw_If_Get_NotFound_ID))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Throw_If_Get_NotFound_ID()
    {
        var exampleId = Guid.NewGuid();
        var dbContext = _fixture.CreateDbContext();

        await dbContext.AddRangeAsync(_fixture.GetExampleAccountsList(15));
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var accountRepository = new Repository.AccountRepository(dbContext);

        var task = async () => await accountRepository.Get(
            exampleId,
            CancellationToken.None
        );

        await task.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Account '{exampleId}' not found.");
    }

    [Fact(DisplayName = nameof(Should_Call_Update_With_Correct_Values))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Call_Update_With_Correct_Values()
    {
        var exampleAccountList = _fixture.GetExampleAccountsList(5);
        var accountExample = exampleAccountList[3];
        var clientType = _fixture.GetRandomClientType();
        var validAccount = _fixture.GetValidAccount(clientType);
        var arrangeContext = _fixture.CreateDbContext();
        await arrangeContext.AddRangeAsync(exampleAccountList);
        await arrangeContext.SaveChangesAsync();
        var dbContext = _fixture.CreateDbContext(true);
        var repository = new Repository.AccountRepository(dbContext);

        accountExample.Update(
            validAccount.ClientType,
            validAccount.DocumentNumber,
            validAccount.Name,
            validAccount.Address,
            validAccount.Phone,
            validAccount.BankName,
            validAccount.AgencyNumber,
            validAccount.AccountNumber,
            validAccount.TaxType,
            validAccount.TaxRate
        );
        await repository.Update(
            accountExample, 
            CancellationToken.None
        );
        await dbContext.SaveChangesAsync();

        var assertionContext = _fixture.CreateDbContext(true);
        var dbAccount = await assertionContext
            .Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == accountExample.Id);

        dbAccount.Should().NotBeNull();
        dbAccount!.Id.Should().NotBeEmpty();
        dbAccount.ClientType.Should().Be(validAccount.ClientType);
        dbAccount.DocumentNumber.Should().Be(validAccount.DocumentNumber);
        dbAccount.Name.Should().Be(validAccount.Name);
        dbAccount.Address.Should().Be(validAccount.Address);
        dbAccount.Phone.Should().Be(validAccount.Phone);
        dbAccount.BankName.Should().Be(validAccount.BankName);
        dbAccount.AgencyNumber.Should().Be(validAccount.AgencyNumber);
        dbAccount.AccountNumber.Should().Be(validAccount.AccountNumber);
        dbAccount.TaxType.Should().Be(validAccount.TaxType);
        dbAccount.TaxRate.Should().Be(validAccount.TaxRate);
    }

    [Fact(DisplayName = nameof(Should_Call_Delete_With_Correct_ID))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Call_Delete_With_Correct_ID()
    {
        var exampleAccountList = _fixture.GetExampleAccountsList(5);
        var accountExample = exampleAccountList[3];
        var arrangeContext = _fixture.CreateDbContext();
        await arrangeContext.AddRangeAsync(exampleAccountList);
        await arrangeContext.SaveChangesAsync();
        var dbContext = _fixture.CreateDbContext(true);
        var repository = new Repository.AccountRepository(dbContext);

        await repository.Delete(
            accountExample, CancellationToken.None
        );
        await dbContext.SaveChangesAsync();
        var assertionContext = _fixture.CreateDbContext(true);
        var itemsInDatabase = assertionContext
            .Accounts
            .AsNoTracking()
            .ToList();

        itemsInDatabase.Should().HaveCount(4);
        itemsInDatabase.Should().NotContain(accountExample);
    }
}