using Bogus;
using Bogus.Extensions.Brazil;

namespace Aparta.Authentication.UnitTests.Domain.Validation.ValidateCPF;

public class ValidateCPFTestDataGenerator
{
    public static IEnumerable<object[]> GetValidsCPFsNumbers(int numberOfTests = 10)
    {
        for (int i = 0; i < numberOfTests; i++)
        {
            var faker = new Faker("pt_BR");
            string cpf = faker.Person.Cpf();

            yield return new object[] { cpf };
        }
    }
}
