using Aparta.Authentication.Infra.Data.EF;
using Repository = Aparta.Authentication.Infra.Data.EF.Repositories;

using FluentAssertions;
using Xunit;
using Aparta.Authentication.Application.Exceptions;

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
        ApartaAuthenticationDbContext dbContext = _fixture.CreateDbContext();
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
        ApartaAuthenticationDbContext dbContext = _fixture.CreateDbContext();
        
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

    [Fact(DisplayName = nameof(Should_Throw_If_ID_NotFound))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Throw_If_ID_NotFound()
    {
        var exampleId = Guid.NewGuid();
        ApartaAuthenticationDbContext dbContext = _fixture.CreateDbContext();

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

    /*[Fact(DisplayName = nameof(Should_Call_Update_With_Correct_Values))]
    [Trait("Integration/Infra.Data", "AccountRepository - Repositories")]
    public async Task Should_Call_Update_With_Correct_Values()
    {
        var clientType = _fixture.GetRandomClientType();
        var validAccount = _fixture.GetValidAccount(clientType);
        var newValidAccount = _fixture.GetValidAccount(clientType);
        var exampleAccountList = _fixture.GetExampleAccountsList(15);
        ApartaAuthenticationDbContext dbContext = _fixture.CreateDbContext();
        var accountRepository = new Repository.AccountRepository(dbContext);
        
        await dbContext.AddRangeAsync(exampleAccountList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        validAccount.Update(
            newValidAccount.ClientType,
            newValidAccount.DocumentNumber,
            newValidAccount.Name,
            newValidAccount.Address,
            newValidAccount.Phone,
            newValidAccount.BankName,
            newValidAccount.AgencyNumber,
            newValidAccount.AccountNumber,
            newValidAccount.TaxType,
            newValidAccount.TaxRate
        );
        await accountRepository.Update(validAccount, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var dbAccount = await (_fixture
            .CreateDbContext(true))
            .Accounts
            .FindAsync(validAccount.Id
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
    }*/
}