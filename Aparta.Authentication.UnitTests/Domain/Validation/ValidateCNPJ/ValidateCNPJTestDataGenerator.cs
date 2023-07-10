using Bogus;
using Bogus.Extensions.Brazil;

namespace Aparta.Authentication.UnitTests.Domain.Validation.ValidateCNPJ;

public class ValidateCNPJTestDataGenerator
{
    public static IEnumerable<object[]> GetValidsCNPJNumbers(int numberOfTests = 10)
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            var faker = new Faker("pt_BR");
            string cnpj = faker.Company.Cnpj();

            yield return new object[] { cnpj };
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
}
