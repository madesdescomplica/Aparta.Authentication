using Aparta.Authentication.UseCases.Account.CreateAccount;

using Aparta.Authentication.IntegrationTests.Application.Common;

using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.CreateAccount;

[CollectionDefinition(nameof(CreateAccountTestFixture))]
public class CreateAccountTestFixtureCollection
    : ICollectionFixture<CreateAccountTestFixture>
{ }

public class CreateAccountTestFixture
    : AccountUseCasesBaseFixture
{
    public CreateAccountInput GetInput()
    {
        var account = GetValidAccount(GetRandomClientType());
        return new CreateAccountInput(
            clientType: account.ClientType,
            documentNumber: account.DocumentNumber,
            name: account.Name,
            address: account.Address,
            phone: account.Phone,
            bankCode: account.BankCode,
            bankName: account.BankName,
            agencyNumber: account.AgencyNumber,
            accountNumber: account.AccountNumber,
            taxType: account.TaxType,
            taxRate: account.TaxRate
        );
    }

    public CreateAccountInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }

    public CreateAccountInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetInput();
        var tooLongNameForAccount = Faker.Person.FullName;
        while (tooLongNameForAccount.Length <= 255)
            tooLongNameForAccount = 
                $"{tooLongNameForAccount} {Faker.Person.FullName}";
        invalidInputTooLongName.Name = tooLongNameForAccount;
        return invalidInputTooLongName;
    }
    
    public CreateAccountInput GetInvalidInputTooLongAddress()
    {
        var invalidInputTooLongAddress = GetInput();
        var tooLongAddressForAccount = Faker.Person.Address.Street;
        while (tooLongAddressForAccount.Length <= 10000)
            tooLongAddressForAccount = 
                $"{tooLongAddressForAccount} {Faker.Person.Address.Street}";
        invalidInputTooLongAddress.Address = tooLongAddressForAccount;
        return invalidInputTooLongAddress;
    }

    public CreateAccountInput GetInvalidInputTooLongBankName()
    {
        var invalidInputTooLongBankName = GetInput();
        var tooLongBankNameForAccount = Faker.Company.CompanyName();
        while (tooLongBankNameForAccount.Length <= 255)
            tooLongBankNameForAccount = 
                $"{tooLongBankNameForAccount} {Faker.Company.CompanyName()}";
        invalidInputTooLongBankName.BankName = tooLongBankNameForAccount;
        return invalidInputTooLongBankName;
    }
}