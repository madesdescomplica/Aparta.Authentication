
using ApplicationUseCases = Aparta.Authentication.Application.UseCases.Account.CreateAccount;

using Aparta.Authentication.Infra.Data.EF.Repositories;
using Aparta.Authentication.Infra.Data.EF;

using FluentAssertions;
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
            .Accounts.FindAsync(output.Id);

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

