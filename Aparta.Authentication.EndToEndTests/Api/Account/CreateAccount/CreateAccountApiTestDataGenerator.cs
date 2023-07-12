namespace Aparta.Authentication.EndToEndTests.Api.Account.CreateAccount;

public class CreateAccountApiTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 20)
    {
        var fixture = new CreateAccountApiTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 10;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputNameEmpty(),
                        "Name should not be empty or null"
                    });
                    break;
                case 1:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should have at least 3 characters"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongName(),
                        "Name should have less or equal 255 characters"
                    });
                    break;
                case 3:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAddressEmpty(),
                        "Address should not be empty or null"
                    });
                    break;
                case 4:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongAddress(),
                        "Address should have less or equal 10000 characters"
                    });
                    break;
                case 5:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputPhoneEmpty(),
                        "Phone should not be empty or null"
                    });
                    break;
                case 6:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputBankNameEmpty(),
                        "BankName should not be empty or null"
                    });
                    break;
                case 7:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongBankName(),
                        "BankName should have less or equal 255 characters"
                    });
                    break;
                case 8:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAgencyNumberEmpty(),
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 9:
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
    
    public static IEnumerable<object[]> GetInvalidInputsNull(int times = 12)
    {
        var fixture = new CreateAccountApiTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 6;

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
                        fixture.GetInvalidInputAddressNull(),
                        "Address should not be empty or null"
                    });
                    break;
                case 2:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputPhoneNull(),
                        "Phone should not be empty or null"
                    });
                    break;
                case 3:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputBankNameNull(),
                        "BankName should not be empty or null"
                    });
                    break;
                case 4:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAgencyNumberNull(),
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 5:
                    invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputAccountNumberNull(),
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
