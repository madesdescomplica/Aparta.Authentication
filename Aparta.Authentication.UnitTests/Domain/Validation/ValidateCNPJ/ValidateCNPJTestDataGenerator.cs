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
}
