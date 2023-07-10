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
            throw new EntityValidationException("Invalid client type");
        }
    }
}
