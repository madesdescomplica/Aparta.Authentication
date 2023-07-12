using Aparta.Authentication.Application.UseCases.Account.CreateAccount;

using Aparta.Authentication.EndToEndTests.Api.Account.Common;

using Xunit;

namespace Aparta.Authentication.EndToEndTests.Api.Account.CreateAccount;

[CollectionDefinition(nameof(CreateAccountApiTestFixture))]
public class CreateAccountApiTestFixtureCollection
    : ICollectionFixture<CreateAccountApiTestFixture>
{ }

public class CreateAccountApiTestFixture
    : AccountBaseFixture
{
    public CreateAccountInput GetInput()
    {
        var clientType = GetRandomClientType();
        return new(
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
    }

    public CreateAccountInput GetInvalidInputNameNull()
    {
        var invalidInputNameNull = GetInput();
        invalidInputNameNull.Name = null!;
        return invalidInputNameNull;
    }

    public CreateAccountInput GetInvalidInputNameEmpty()
    {
        var invalidInputNameEmpty = GetInput();
        invalidInputNameEmpty.Name = "";
        return invalidInputNameEmpty;
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
            tooLongNameForAccount = $"{tooLongNameForAccount} {Faker.Person.FullName}";
        invalidInputTooLongName.Name = tooLongNameForAccount;
        return invalidInputTooLongName;
    }

    public CreateAccountInput GetInvalidInputAddressNull()
    {
        var invalidInputAddressNull = GetInput();
        invalidInputAddressNull.Address = null!;
        return invalidInputAddressNull;
    }

    public CreateAccountInput GetInvalidInputAddressEmpty()
    {
        var invalidInputAddressEmpty = GetInput();
        invalidInputAddressEmpty.Address = "";
        return invalidInputAddressEmpty;
    }

    public CreateAccountInput GetInvalidInputTooLongAddress()
    {
        var invalidInputTooLongAddress = GetInput();
        var tooLongAddressForAccount = Faker.Person.Address.Street;
        while (tooLongAddressForAccount.Length <= 10000)
            tooLongAddressForAccount = $"{tooLongAddressForAccount} {Faker.Person.Address.Street}";
        invalidInputTooLongAddress.Address = tooLongAddressForAccount;
        return invalidInputTooLongAddress;
    }

    public CreateAccountInput GetInvalidInputPhoneNull()
    {
        var invalidInputPhoneNull = GetInput();
        invalidInputPhoneNull.Phone = null!;
        return invalidInputPhoneNull;
    }

    public CreateAccountInput GetInvalidInputPhoneEmpty()
    {
        var invalidInputPhoneEmpty = GetInput();
        invalidInputPhoneEmpty.Phone = "";
        return invalidInputPhoneEmpty;
    }

    public CreateAccountInput GetInvalidInputBankNameNull()
    {
        var invalidInputBankNameNull = GetInput();
        invalidInputBankNameNull.BankName = null!;
        return invalidInputBankNameNull;
    }

    public CreateAccountInput GetInvalidInputBankNameEmpty()
    {
        var invalidInputBankNameEmpty = GetInput();
        invalidInputBankNameEmpty.BankName = "";
        return invalidInputBankNameEmpty;
    }

    public CreateAccountInput GetInvalidInputTooLongBankName()
    {
        var invalidInputTooLongBankName = GetInput();
        var tooLongBankNameForAccount = Faker.Company.CompanyName();
        while (tooLongBankNameForAccount.Length <= 255)
            tooLongBankNameForAccount = $"{tooLongBankNameForAccount} {Faker.Company.CompanyName()}";
        invalidInputTooLongBankName.BankName = tooLongBankNameForAccount;
        return invalidInputTooLongBankName;
    }

    public CreateAccountInput GetInvalidInputAgencyNumberNull()
    {
        var invalidInputAgencyNumberNull = GetInput();
        invalidInputAgencyNumberNull.AgencyNumber = null!;
        return invalidInputAgencyNumberNull;
    }

    public CreateAccountInput GetInvalidInputAgencyNumberEmpty()
    {
        var invalidInputAgencyNumberEmpty = GetInput();
        invalidInputAgencyNumberEmpty.AgencyNumber = "";
        return invalidInputAgencyNumberEmpty;
    }

    public CreateAccountInput GetInvalidInputAccountNumberNull()
    {
        var invalidInputAccountNumberNull = GetInput();
        invalidInputAccountNumberNull.AccountNumber = null!;
        return invalidInputAccountNumberNull;
    }

    public CreateAccountInput GetInvalidInputAccountNumberEmpty()
    {
        var invalidInputAccountNumberEmpty = GetInput();
        invalidInputAccountNumberEmpty.AccountNumber = "";
        return invalidInputAccountNumberEmpty;
    }
}
