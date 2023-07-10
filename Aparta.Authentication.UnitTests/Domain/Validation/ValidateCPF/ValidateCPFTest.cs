using DomainValidation = Aparta.Authentication.Domain.Validation;

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
        var isValid = DomainValidation.ValidateCPF.IsValid(cpf);

        isValid.Should().BeTrue();
    }
}