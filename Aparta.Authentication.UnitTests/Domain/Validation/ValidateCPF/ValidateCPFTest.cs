using Validations = Aparta.Authentication.Domain.Validation;

using FluentAssertions;
using Xunit;

namespace Aparta.Authentication.UnitTests.Domain.Validation.ValidateCPF;
public class ValidateCPFTest
{
    [Theory(DisplayName = nameof(Should_Return_True_If_Receive_Correct_CPF_Number))]
    [Trait("Domain", "ValidateCPF - Validation")]
    [MemberData(
        nameof(ValidateCPFTestDataGenerator.GetValidsCPFsNumbers),
    parameters: 10,
        MemberType = typeof(ValidateCPFTestDataGenerator)
    )]
    public void Should_Return_True_If_Receive_Correct_CPF_Number(string cpf)
    {
        var isValid = Validations.ValidateCPF.IsValid(cpf);

        isValid.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(Should_Return_False_If_Receive_Incorrect_CPF_Number))]
    [Trait("Domain", "ValidateCPF - Validation")]
    [MemberData(
        nameof(ValidateCPFTestDataGenerator.GetInvalidsCPFsNumbers),
        parameters: 10,
        MemberType = typeof(ValidateCPFTestDataGenerator)
    )]
    public void Should_Return_False_If_Receive_Incorrect_CPF_Number(string cpf)
    {
        var isValid = Validations.ValidateCPF.IsValid(cpf);

        isValid.Should().BeFalse();
    }

    [Theory(DisplayName = nameof(Should_Return_False_If_Receive_Incorrect_CPF_Number))]
    [Trait("Domain", "ValidateCPF - Validation")]
    [InlineData("00000000000")]
    [InlineData("11111111111")]
    [InlineData("22222222222")]
    public void Should_Return_False_If_Receive_Incorrect_CPF_Number_With_Repeat_Numbers(string cpf)
    {
        var isValid = Validations.ValidateCPF.IsValid(cpf);

        isValid.Should().BeFalse();
    }
}