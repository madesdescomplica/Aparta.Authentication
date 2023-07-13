namespace Aparta.Authentication.EndToEndTests.Api.Account.UpdateAccount;

public class UpdateAccountApiTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 22)
    {
        var fixture = new UpdateAccountApiTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 11;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    var inputNameEmpty = fixture.GetInput();
                    inputNameEmpty.Name = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputNameEmpty,
                        "Name should not be empty or null"
                    });
                    break;
                case 1:
                    var inputNameShort = fixture.GetInput();
                    inputNameShort.Name = fixture.GetInvalidInputShortName();
                    invalidInputsList.Add(new object[] {
                        inputNameShort,
                        "Name should have at least 3 characters"
                    });
                    break;
                case 2:
                    var inputNameLong = fixture.GetInput();
                    inputNameLong.Name = fixture.GetInvalidInputTooLongName();
                    invalidInputsList.Add(new object[] {
                        inputNameLong,
                        "Name should have less or equal 255 characters"
                    });
                    break;
                case 3:
                    var inputAddressEmpty = fixture.GetInput();
                    inputAddressEmpty.Address = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputAddressEmpty,
                        "Address should not be empty or null"
                    });
                    break;
                case 4:
                    var inputAddressLong = fixture.GetInput();
                    inputAddressLong.Address = fixture.GetInvalidInputTooLongAddress();
                    invalidInputsList.Add(new object[] {
                        inputAddressLong,
                        "Address should have less or equal 10000 characters"
                    });
                    break;
                case 5:
                    var inputPhoneEmpty = fixture.GetInput();
                    inputPhoneEmpty.Phone = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputPhoneEmpty,
                        "Phone should not be empty or null"
                    });
                    break;
                case 6:
                    var inputBankCodeEmpty = fixture.GetInput();
                    inputBankCodeEmpty.BankCode = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputBankCodeEmpty,
                        "BankCode should not be empty or null"
                    });
                    break;
                case 7:
                    var inputBankNameEmpty = fixture.GetInput();
                    inputBankNameEmpty.BankName = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputBankNameEmpty,
                        "BankName should not be empty or null"
                    });
                    break;
                case 8:
                    var inputBankNameLong = fixture.GetInput();
                    inputBankNameLong.BankName =
                        fixture.GetInvalidInputTooLongBankName();
                    invalidInputsList.Add(new object[] {
                        inputBankNameLong,
                        "BankName should have less or equal 255 characters"
                    });
                    break;
                case 9:
                    var inputAgencyNumberEmpty = fixture.GetInput();
                    inputAgencyNumberEmpty.AgencyNumber = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputAgencyNumberEmpty,
                        "AgencyNumber should not be empty or null"
                    });
                    break;
                case 10:
                    var inputAccountNumberEmpty = fixture.GetInput();
                    inputAccountNumberEmpty.AccountNumber = string.Empty;
                    invalidInputsList.Add(new object[] {
                        inputAccountNumberEmpty,
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
