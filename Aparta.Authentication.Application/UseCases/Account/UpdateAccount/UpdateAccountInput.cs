using Aparta.Authentication.Domain.Enum;
using Aparta.Authentication.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.Account.UpdateAccount;

public class UpdateAccountInput : IRequest<AccountModelOutput>
{
    public Guid Id { get; set; }
    public ClientType ClientType { get; set; }
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string BankCode { get; set; }
    public string BankName { get; set; }
    public string AgencyNumber { get; set; }
    public string AccountNumber { get; set; }
    public TaxType TaxType { get; set; }
    public float TaxRate { get; set; }

    public UpdateAccountInput(
        Guid id,
        ClientType clientType,
        string documentNumber,
        string name,
        string address,
        string phone,
        string bankCode,
        string bankName,
        string agencyNumber,
        string accountNumber,
        TaxType taxType,
        float taxRate
     )
    {
        Id = id;
        ClientType = clientType;
        DocumentNumber = documentNumber;
        Name = name;
        Address = address;
        Phone = phone;
        BankCode = bankCode;
        BankName = bankName;
        AgencyNumber = agencyNumber;
        AccountNumber = accountNumber;
        TaxType = taxType;
        TaxRate = taxRate;
    }
}