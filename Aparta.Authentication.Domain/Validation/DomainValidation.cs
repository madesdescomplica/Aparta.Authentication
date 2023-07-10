using Aparta.Authentication.Domain.Enum;
using Aparta.Authentication.Domain.Exceptions;

namespace Aparta.Authentication.Domain.Validation;

public class DomainValidation
{
    public static void ValidateCPFAndCNPJ(ClientType clientType, string documentNumber)
    {
        if (clientType == ClientType.PF)
        {
            if (!ValidateCPF.IsValid(documentNumber))
                throw new EntityValidationException("Invalid CPF number");
        }
        else if (clientType == ClientType.PJ)
        {
            if (!ValidateCNPJ.IsValid(documentNumber))
                throw new EntityValidationException("Invalid CNPJ number");
        }
        else
        {
            throw new EntityValidationException("Invalid ClientType");
        }
    }

    public static void NotNull(object? target, string fieldName)
    {
        if (target == null)
        {
            throw new EntityValidationException(
                $"{fieldName} should not be null"
            );
        }
    }

    public static void NotNullOrEmpty(string? target, string fieldname)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new EntityValidationException(
                $"{fieldname} should not be empty or null"
            );
        }
    }

    public static void MinLength(string target, int minLength, string fieldname)
    {
        if (target.Length < minLength)
        {
            throw new EntityValidationException(
                $"{fieldname} should have at least {minLength} characters"
            );
        }
    }
}
