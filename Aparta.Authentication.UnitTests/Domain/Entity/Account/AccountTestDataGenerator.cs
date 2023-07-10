namespace Aparta.Authentication.UnitTests.Domain.Entity.Account;

public class AccountTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidsCPFsNumbers(int numberOfTests = 10)
    {

        for (int i = 0; i < numberOfTests; i++)
        {
            var random = new Random();
            int randomNumber = random.Next(30);
            string cpf = "";
            string numbers = "0123456789";

            // Gerate a random number between 0 and 30 digits
            for (int j = 0; j < randomNumber; j++)
            {
                int index = new Random().Next(0, numbers.Length);
                cpf += numbers[index];
            }

            if (cpf.Length != 11)
            {
                yield return new object[] {
                    cpf
                };
            }
            else
            {
                yield return new object[]
                {
                    cpf + "1"
                };
            }
        }
    }

    public static IEnumerable<object[]> GetInvalidsCNPJsNumbers(int numberOfTests = 10)
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            var random = new Random();
            int randomNumber = random.Next(30);
            string cpf = "";
            string numbers = "0123456789";

            // Gerate a random number between 0 and 30 digits
            for (int j = 0; j < randomNumber; j++)
            {
                int index = new Random().Next(0, numbers.Length);
                cpf += numbers[index];
            }

            if (cpf.Length != 14)
            {
                yield return new object[] {
                    cpf
                };
            }
            else
            {
                yield return new object[]
                {
                    cpf + "1"
                };
            }
        }
    }

    public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
    {
        var fixture = new AccountTestFixture();
        for (int i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return new object[] {
                fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)]
            };
        }
    }
}
