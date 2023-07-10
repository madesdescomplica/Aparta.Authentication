using DomainValidation = Aparta.Authentication.Domain.Validation;

using FluentAssertions;
using Xunit;

namespace Aparta.Authentication.UnitTests.Domain.Validation.ValidateCNPJ;

public class ValidateCNPJTest
{
    [Theory(DisplayName = nameof(Should_Return_True_If_Receive_Correct_CNPJ_Number))]
    [Trait("Domain", "ValidateCNPJ - Validation")]
    [MemberData(
        nameof(ValidateCNPJTestDataGenerator.GetValidsCNPJNumbers),
    parameters: 10,
        MemberType = typeof(ValidateCNPJTestDataGenerator)
    )]
    public void Should_Return_True_If_Receive_Correct_CNPJ_Number(string cnpj)
    {
        var isValid = DomainValidation.ValidateCNPJ.IsValid(cnpj);

        isValid.Should().BeTrue();
    }
}
