using Aparta.Authentication.Domain.Enum;
using Validations = Aparta.Authentication.Domain.Validation;

using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Xunit;

namespace Aparta.Authentication.UnitTests.Domain.Validation.DomainValidation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(Should_Not_Throw_If_Receive_Correct_ClientType_PF))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Not_Throw_If_Receive_Correct_ClientType_PF()
    {
        var clientType = ClientType.PF;
        string documentNumber = Faker.Person.Cpf();
        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should().NotThrow();
    }
}
