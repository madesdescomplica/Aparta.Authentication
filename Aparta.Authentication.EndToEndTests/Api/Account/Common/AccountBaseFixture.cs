using Aparta.Authentication.Domain.Enum;
using DomainEntity = Aparta.Authentication.Domain.Entity;

using Aparta.Authentication.EndToEndTests.Base;

using Bogus.Extensions.Brazil;

namespace Aparta.Authentication.EndToEndTests.Api.Account.Common;

public class AccountBaseFixture
    : BaseFixture
{
    public AccountPersistence Persistence;

    public AccountBaseFixture() : base()
    {
        Persistence = new AccountPersistence(
            CreateDbContext()
        );
    }

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

    public string GetValidName()
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

    public DomainEntity.Account GetValidAccount(ClientType clientType)
        => new(
            clientType,
            GetRandomDocumentNumber(clientType),
            GetValidName(),
            GetValidAddress(),
            GetValidPhone(),
            GetValidBankName(),
            GetValidAgencyNumber(),
            GetValidAccountNumber(),
            GetRandomTaxType(),
            GetValidTaxRate()
        );

    public List<DomainEntity.Account> GetExampleAccountsList(int length = 10)
        => Enumerable
            .Range(1, length)
            .Select(_ => GetValidAccount(GetRandomClientType()))
            .ToList();

    public string GetInvalidInputShortName() 
        => Faker.Person.FirstName[..2];

    public string GetInvalidInputTooLongName()
    {
        var tooLongNameForCategory = Faker.Person.FullName;
        while (tooLongNameForCategory.Length <= 255)
            tooLongNameForCategory = 
                $"{tooLongNameForCategory} {Faker.Person.FullName}";
        return tooLongNameForCategory;
    }

    public string GetInvalidInputTooLongAddress()
    {
        var tooLongAddressForAccount = Faker.Person.Address.Street;
        while (tooLongAddressForAccount.Length <= 10000)
            tooLongAddressForAccount =
                $"{tooLongAddressForAccount} {Faker.Person.Address.Street}";
        return tooLongAddressForAccount;
    }

    public string GetInvalidInputTooLongBankName()
    {
        var tooLongBankNameForAccount = Faker.Company.CompanyName();
        while (tooLongBankNameForAccount.Length <= 255)
            tooLongBankNameForAccount = 
                $"{tooLongBankNameForAccount} {Faker.Company.CompanyName()}";
        return tooLongBankNameForAccount;
    }
}
