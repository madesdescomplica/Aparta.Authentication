using Aparta.Authentication.UseCases.Exceptions;
using UseCase = Aparta.Authentication.UseCases.Account.GetAccount;

using Aparta.Authentication.Infra.Data.EF.Repositories;

using FluentAssertions;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.GetAccount;

[Collection(nameof(GetAccountTestFixture))]
public class GetAccountTest
{
    private readonly GetAccountTestFixture _fixture;

    public GetAccountTest(GetAccountTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_GetAccount_With_Correct_Values))]
    [Trait("Integration/Application", "GetAccount - UseCases")]
    public async Task Should_GetAccount_With_Correct_Values()
    {
        var clientType = _fixture.GetRandomClientType();
        var account = _fixture.GetValidAccount(clientType);
        var dbContext = _fixture.CreateDbContext();
        dbContext.Accounts.Add(account);
        dbContext.SaveChanges();
        var repository = new AccountRepository(dbContext);
        var input = new UseCase.GetAccountInput(account.Id);
        var useCase = new UseCase.GetAccount(repository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output!.Id.Should().NotBeEmpty();
        output.ClientType.Should().Be(account.ClientType);
        output.DocumentNumber.Should().Be(account.DocumentNumber);
        output.Name.Should().Be(account.Name);
        output.Address.Should().Be(account.Address);
        output.Phone.Should().Be(account.Phone);
        output.BankCode.Should().Be(account.BankCode);
        output.BankName.Should().Be(account.BankName);
        output.AgencyNumber.Should().Be(account.AgencyNumber);
        output.AccountNumber.Should().Be(account.AccountNumber);
        output.TaxType.Should().Be(account.TaxType);
        output.TaxRate.Should().Be(account.TaxRate);
        output.CreatedAt.Should().NotBe(default);
    }

    [Fact(DisplayName = nameof(Should_Throw_NotFoundException_When_Account_Doesnt_Exist))]
    [Trait("Integration/Application", "GetAccount - UseCases")]
    public async Task Should_Throw_NotFoundException_When_Account_Doesnt_Exist()
    {
        var clientType = _fixture.GetRandomClientType();
        var account = _fixture.GetValidAccount(clientType);
        var dbContext = _fixture.CreateDbContext();
        dbContext.Accounts.Add(account);
        dbContext.SaveChanges();
        var repository = new AccountRepository(dbContext);
        var input = new UseCase.GetAccountInput(Guid.NewGuid());
        var useCase = new UseCase.GetAccount(repository);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Account '{input.Id}' not found.");
    }
}