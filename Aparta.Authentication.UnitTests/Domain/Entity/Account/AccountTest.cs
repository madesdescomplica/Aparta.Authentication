﻿using DomainEntity = Aparta.Authentication.Domain.Entity;

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
}
