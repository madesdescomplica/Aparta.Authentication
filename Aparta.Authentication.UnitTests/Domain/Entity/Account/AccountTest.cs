using DomainEntity = Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Domain.Enum;
using Aparta.Authentication.Domain.Exceptions;

using FluentAssertions;
using Xunit;

namespace Aparta.Authentication.UnitTests.Domain.Entity.Account;

[Collection(nameof(AccountTestFixture))]
public class AccountTest
{
    private readonly AccountTestFixture _fixture;

    public AccountTest(AccountTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_Call_Account_With_Correct_Values))]
    [Trait("Domain", "Account - Aggregates")]
    public void Should_Call_Account_With_Correct_Values()
    {
        var clientType = _fixture.GetRandomClientType();
        var validAccount = _fixture.GetValidAccount(clientType);
        var datetimeBefore = DateTime.Now;

        var account = new DomainEntity.Account(
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
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        account.Should().NotBeNull();
        account.ClientType.Should().Be(validAccount.ClientType);
        account.DocumentNumber.Should().Be(validAccount.DocumentNumber);
        account.Name.Should().Be(validAccount.Name);
        account.Address.Should().Be(validAccount.Address);
        account.Phone.Should().Be(validAccount.Phone);
        account.BankName.Should().Be(validAccount.BankName);
        account.AgencyNumber.Should().Be(validAccount.AgencyNumber);
        account.AccountNumber.Should().Be(validAccount.AccountNumber);
        account.TaxType.Should().Be(validAccount.TaxType);
        account.TaxRate.Should().Be(validAccount.TaxRate);
        account.Id.Should().NotBeEmpty();
        account.CreatedAt.Should().NotBeSameDateAs(default);
        (account.CreatedAt >= datetimeBefore).Should().BeTrue();
        (account.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Should_Receive_Correct_CPF_Number))]
    [Trait("Domain", "Account - Aggregates")]
    [InlineData(ClientType.PF)]
    public void Should_Receive_Correct_CPF_Number(ClientType clientType)
    {
        var validAccount = _fixture.GetValidAccount(clientType);

        var account = new DomainEntity.Account(
            clientType,
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

        account.Should().NotBeNull();
        account.ClientType.Should().Be(clientType);
        account.DocumentNumber.Should().Be(validAccount.DocumentNumber);
        account.DocumentNumber.Should().HaveLength(14);
    }

    [Theory(DisplayName = nameof(Should_Throw_If_Receive_Incorrect_CPF_Number))]
    [Trait("Domain", "Account - Aggregates")]
    [MemberData(
        nameof(AccountTestDataGenerator.GetInvalidsCPFsNumbers),
        parameters: 10,
        MemberType = typeof(AccountTestDataGenerator)
    )]
    public void Should_Throw_If_Receive_Incorrect_CPF_Number(string documentNumber)
    {
        var clientType = ClientType.PF;
        var validAccount = _fixture.GetValidAccount(clientType);

        Action action = ()
            => new DomainEntity.Account(
            clientType,
            documentNumber,
            validAccount.Name,
            validAccount.Address,
            validAccount.Phone,
            validAccount.BankName,
            validAccount.AgencyNumber,
            validAccount.AccountNumber,
            validAccount.TaxType,
            validAccount.TaxRate
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid CPF number");
    }

    [Theory(DisplayName = nameof(Should_Receive_Correct_CNPJ_Number))]
    [Trait("Domain", "Account - Aggregates")]
    [InlineData(ClientType.PJ)]
    public void Should_Receive_Correct_CNPJ_Number(ClientType clientType)
    {
        var validAccount = _fixture.GetValidAccount(clientType);

        var account = new DomainEntity.Account(
            clientType,
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

        account.Should().NotBeNull();
        account.ClientType.Should().Be(clientType);
        account.DocumentNumber.Should().Be(validAccount.DocumentNumber);
        account.DocumentNumber.Should().HaveLength(18);
    }

    [Theory(DisplayName = nameof(Should_Throw_If_Receive_Incorrect_CNPJ_Number))]
    [Trait("Domain", "Account - Aggregates")]
    [MemberData(
        nameof(AccountTestDataGenerator.GetInvalidsCNPJsNumbers),
        parameters: 10,
        MemberType = typeof(AccountTestDataGenerator)
    )]
    public void Should_Throw_If_Receive_Incorrect_CNPJ_Number(string documentNumber)
    {
        var clientType = ClientType.PJ;
        var validAccount = _fixture.GetValidAccount(clientType);

        Action action = ()
            => new DomainEntity.Account(
            clientType,
            documentNumber,
            validAccount.Name,
            validAccount.Address,
            validAccount.Phone,
            validAccount.BankName,
            validAccount.AgencyNumber,
            validAccount.AccountNumber,
            validAccount.TaxType,
            validAccount.TaxRate
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid CNPJ number");
    }

    [Theory(DisplayName = nameof(Should_Throw_An_Error_When_Name_Is_Empty))]
    [Trait("Domain", "Account - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Should_Throw_An_Error_When_Name_Is_Empty(string? name)
    {
        var clientType = _fixture.GetRandomClientType();
        var validAccount = _fixture.GetValidAccount(clientType);

        Action action = ()
            => new DomainEntity.Account(
            clientType,
            validAccount.DocumentNumber,
            name!,
            validAccount.Address,
            validAccount.Phone,
            validAccount.BankName,
            validAccount.AgencyNumber,
            validAccount.AccountNumber,
            validAccount.TaxType,
            validAccount.TaxRate
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }
}
