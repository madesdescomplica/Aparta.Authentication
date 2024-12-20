﻿using Validations = Aparta.Authentication.Domain.Validation;

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
        var isValid = Validations.ValidateCNPJ.IsValid(cnpj);

        isValid.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Should_Return_False_If_Receive_Incorrect_CNPJ_Number))]
    [Trait("Domain", "ValidateCNPJ - Validation")]
    [MemberData(
        nameof(ValidateCNPJTestDataGenerator.GetInvalidsCNPJsNumbers),
        parameters: 10,
        MemberType = typeof(ValidateCNPJTestDataGenerator)
    )]
    public void Should_Return_False_If_Receive_Incorrect_CNPJ_Number(string cnpj)
    {
        var isValid = Validations.ValidateCPF.IsValid(cnpj);

        isValid.Should().BeFalse();
    }
}
