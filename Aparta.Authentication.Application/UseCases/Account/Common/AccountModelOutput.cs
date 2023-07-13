using Aparta.Authentication.Domain.Enum;

namespace Aparta.Authentication.UseCases.Account.Common;

public class AccountModelOutput
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
    public DateTime CreatedAt { get; set; }

    public AccountModelOutput(
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
        float taxRate,
        DateTime createdAt
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
        CreatedAt = createdAt;
    }

    public static AccountModelOutput FromAccount(Domain.Entity.Account account)
        => new(
            id: account.Id,
            clientType: account.ClientType,
            documentNumber: account.DocumentNumber,
            name: account.Name,
            address: account.Address,
            phone: account.Phone,
            bankCode: account.BankCode,
            bankName: account.BankName,
            agencyNumber: account.AgencyNumber,
            accountNumber: account.AccountNumber,
            taxType: account.TaxType,
            taxRate: account.TaxRate,
            createdAt: account.CreatedAt
        );
}