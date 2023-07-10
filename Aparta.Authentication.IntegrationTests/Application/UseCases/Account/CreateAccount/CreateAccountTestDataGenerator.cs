namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.CreateAccount;

public class CreateAccountTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 48)
    {
        var fixture = new CreateAccountTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 16;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputNameNull(),
                        "Name should not be empty or null"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputNameEmpty(),
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
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAddressNull(),
                        "Address should not be empty or null"
                    });
                    break;
                case 5:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAddressEmpty(),
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
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputPhoneNull(),
                        "Phone should not be empty or null"
                    });
                    break;
                case 8:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputPhoneEmpty(),
                        "Phone should not be empty or null"
                    });
                    break;
                case 9:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputBankNameNull(),
                        "BankName should not be empty or null"
                    });
                    break;
                case 10:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputBankNameEmpty(),
                        "BankName should not be empty or null"
                    });
                    break;
                case 11:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongBankName(),
                        "BankName should have less or equal 255 characters"
                    });
                    break;
                case 12:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAgencyNumberNull(),
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 13:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAgencyNumberEmpty(),
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 14:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAccountNumberNull(),
                        "AccountNumber should not be empty or null"
                    });
                    break;
                case 15:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAccountNumberEmpty(),
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
