namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.CreateAccount;

public class CreateAccountTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 36)
    {
        var fixture = new CreateAccountTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 18;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    var invalidInputNameNull = fixture.GetInput();
                    invalidInputNameNull.Name = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputNameNull,
                        "Name should not be empty or null"
                    });
                    break;
                case 1:
                    var invalidInputNameEmpty = fixture.GetInput();
                    invalidInputNameEmpty.Name = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputNameEmpty,
                        "Name should not be empty or null"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should have at least 3 characters"
                    });
                    break;
                case 3:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should have less or equal 255 characters"
                    });
                    break;
                case 4:
                    var invalidInputAddressNull = fixture.GetInput();
                    invalidInputAddressNull.Address = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputAddressNull,
                        "Address should not be empty or null"
                    });
                    break;
                case 5:
                    var invalidInputAddressEmpty = fixture.GetInput();
                    invalidInputAddressEmpty.Address = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputAddressEmpty,
                        "Address should not be empty or null"
                    });
                    break;
                case 6:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongAddress(),
                        "Address should have less or equal 10000 characters"
                    });
                    break;
                case 7:
                    var invalidInputPhoneNull = fixture.GetInput();
                    invalidInputPhoneNull.Phone = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputPhoneNull,
                        "Phone should not be empty or null"
                    });
                    break;
                case 8:
                    var invalidInputPhoneEmpty = fixture.GetInput();
                    invalidInputPhoneEmpty.Phone = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputPhoneEmpty,
                        "Phone should not be empty or null"
                    });
                    break;
                case 9:
                    var invalidInputBankCodeNull = fixture.GetInput();
                    invalidInputBankCodeNull.BankCode = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputBankCodeNull,
                        "BankCode should not be empty or null"
                    });
                    break;
                case 10:
                    var invalidInputBankCodeEmpty = fixture.GetInput();
                    invalidInputBankCodeEmpty.BankCode = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputBankCodeEmpty,
                        "BankCode should not be empty or null"
                    });
                    break;
                case 11:
                    var invalidInputBankNameNull = fixture.GetInput();
                    invalidInputBankNameNull.BankName = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputBankNameNull,
                        "BankName should not be empty or null"
                    });
                    break;
                case 12:
                    var invalidInputBankNameEmpty = fixture.GetInput();
                    invalidInputBankNameEmpty.BankName = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputBankNameEmpty,
                        "BankName should not be empty or null"
                    });
                    break;
                case 13:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongBankName(),
                        "BankName should have less or equal 255 characters"
                    });
                    break;
                case 14:
                    var invalidInputAgencyNumberNull = fixture.GetInput();
                    invalidInputAgencyNumberNull.AgencyNumber = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputAgencyNumberNull,
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 15:
                    var invalidInputAgencyNumberEmpty = fixture.GetInput();
                    invalidInputAgencyNumberEmpty.AgencyNumber = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputAgencyNumberEmpty,
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 16:
                    var invalidInputAccountNumberNull = fixture.GetInput();
                    invalidInputAccountNumberNull.AccountNumber = null!;
                    invalidInputsList.Add(new object[] {
                        invalidInputAccountNumberNull,
                        "AccountNumber should not be empty or null"
                    });
                    break;
                case 17:
                    var invalidInputAccountNumberEmpty = fixture.GetInput();
                    invalidInputAccountNumberEmpty.AccountNumber = string.Empty;
                    invalidInputsList.Add(new object[] {
                        invalidInputAccountNumberEmpty,
                        "AccountNumber should not be empty or null"
                    });
                    break;
                default:
                    break;
            }
        }

        return invalidInputsList;
    }
}
