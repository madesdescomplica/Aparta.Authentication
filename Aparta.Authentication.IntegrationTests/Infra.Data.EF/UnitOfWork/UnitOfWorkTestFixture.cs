using Aparta.Authentication.Domain.Entity;
using Aparta.Authentication.Domain.Enum;

using Aparta.Authentication.IntegrationTests.Base;

using Bogus;
using Bogus.Extensions.Brazil;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Infra.Data.EF.UnitOfWork;

[CollectionDefinition(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTestFixtureCollection
    : ICollectionFixture<UnitOfWorkTestFixture>
{ }

public class UnitOfWorkTestFixture : BaseFixture
{
    public ClientType GetRandomClientType()
    {
        List<ClientType> clientTypes = new()
        {
            ClientType.PF,
            ClientType.PJ
        };

        return clientTypes[new Random().Next(
            0,
            clientTypes.Count
        )];
    }

    public string GetRandomDocumentNumber(ClientType clientType)
        => clientType switch
        {
            ClientType.PF => Faker.Person.Cpf(),
            ClientType.PJ => Faker.Company.Cnpj(),
            _ => throw new NotImplementedException()
        };

    public string GetValidCategoryName()
    {
        var accountName = "";

        while (accountName.Length < 3)
            accountName = Faker.Person.FullName;

        if (accountName.Length > 255)
            accountName = accountName[..255];

        return accountName;
    }

    public string GetValidAddress()
        => Faker.Address.StreetAddress();

    public string GetValidPhone()
        => Faker.Phone.PhoneNumber();

    public string GetValidBankName()
    {
        var bankName = "";

        while (bankName.Length < 3)
            bankName = Faker.Company.CompanyName();

        if (bankName.Length > 255)
            bankName = bankName[..255];

        return bankName;
    }

    public string GetValidAgencyNumber()
        => Faker.Finance.Account(5);

    public string GetValidAccountNumber()
        => Faker.Finance.Account(7);

    public TaxType GetRandomTaxType()
    {
        List<TaxType> taxTypes = new()
        {
            TaxType.TaxFixed,
            TaxType.TaxPercentage
        };

        return taxTypes[new Random().Next(
            0,
            taxTypes.Count
        )];
    }

    public float GetValidTaxRate()
        => (float)Faker.Random.Double(0, 101);

    public Account GetValidAccount(ClientType clientType)
        => new(
            clientType,
            GetRandomDocumentNumber(clientType),
            GetValidCategoryName(),
            GetValidAddress(),
            GetValidPhone(),
            GetValidBankName(),
            GetValidAgencyNumber(),
            GetValidAccountNumber(),
            GetRandomTaxType(),
            GetValidTaxRate()
        );

    public List<Account> GetExampleAccountsList(int length = 10)
        => Enumerable
            .Range(1, length)
            .Select(_ => GetValidAccount(GetRandomClientType()))
            .ToList();
}
