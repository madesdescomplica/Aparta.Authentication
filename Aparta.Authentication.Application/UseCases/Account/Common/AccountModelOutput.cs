using Aparta.Authentication.Domain.Enum;

namespace Aparta.Authentication.Application.UseCases.Account.Common;

public class AccountModelOutput
{
    public Guid Id { get; set; }
    public ClientType ClientType { get; set; }
    public string DocumentNumber { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
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
        BankName = bankName;
        AgencyNumber = agencyNumber;
        AccountNumber = accountNumber;
        TaxType = taxType;
        TaxRate = taxRate;
        CreatedAt = createdAt;
    }

    public static AccountModelOutput FromAccount(Domain.Entity.Account account)
        => new(
            account.Id,
            account.ClientType,
            account.DocumentNumber,
            account.Name,
            account.Address,
            account.Phone,
            account.BankName,
            account.AgencyNumber,
            account.AccountNumber,
            account.TaxType,
            account.TaxRate,
            account.CreatedAt
        );
}