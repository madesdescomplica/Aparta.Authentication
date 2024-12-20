﻿using Bogus;
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
}
