using Bogus;

namespace Aparta.Authentication.UnitTests.Domain.Validation.DomainValidation;

public class DomainValidationTestDataGenerator
{
    public static IEnumerable<object[]> GetValuesSmallerThanTheMin(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 10 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Lorem.Word();
            var minLength = example.Length + (new Random()).Next(1, 20);

            yield return new object[] { example, minLength };
        }
    }

    public static IEnumerable<object[]> GetValuesGreaterThanTheMin(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Lorem.Word();
            var minLength = example.Length - (new Random()).Next(1, 5);

            yield return new object[] { example, minLength };
        }
    }

    public static IEnumerable<object[]> GetValuesLessThanTheMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Lorem.Word();
            var maxLength = example.Length + (new Random()).Next(0, 5);

            yield return new object[] { example, maxLength };
        }
    }

    public static IEnumerable<object[]> GetValuesGreaterThanTheMax(int numberOfTests = 5)
    {
        yield return new object[] { "123456", 5 };
        var faker = new Faker();
        for (int i = 0; i < (numberOfTests - 1); i++)
        {
            var example = faker.Lorem.Word();
            var maxLength = example.Length - (new Random()).Next(1, 5);

            yield return new object[] { example, maxLength };
        }
    }
}
