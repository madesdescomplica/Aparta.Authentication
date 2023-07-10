using Aparta.Authentication.Domain.Enum;
using Validations = Aparta.Authentication.Domain.Validation;

using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Xunit;
using Aparta.Authentication.Domain.Exceptions;

namespace Aparta.Authentication.UnitTests.Domain.Validation.DomainValidation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(Should_Not_Throw_If_Receive_Correct_CPF))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Not_Throw_If_Receive_Correct_CPF()
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

    [Fact(DisplayName = nameof(Should_Throw_If_Receive_Incorrect_CPF))]
    [Trait("Domain", "Helpers - Validation")]
    public void Should_Throw_If_Receive_Incorrect_CPF()
    {
        var clientType = ClientType.PF;
        string documentNumber = Faker.Person.Cpf() + "1";

        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid CPF number");
    }

    [Fact(DisplayName = nameof(Should_Not_Throw_If_Receive_Correct_CNPJ))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Not_Throw_If_Receive_Correct_CNPJ()
    {
        var clientType = ClientType.PJ;
        string documentNumber = Faker.Company.Cnpj();
        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should().NotThrow();
    }
}