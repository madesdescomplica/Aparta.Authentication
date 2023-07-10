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
    public BankData BankData { get; set; }
    public string TaxType { get; set; }
    public float TaxRate { get; set; }
    public DateTime CreatedAt { get; set; }

    public AccountModelOutput(
        Guid id,
        string clientType,
        string documentNumber,
        string name,
        string address,
        string phone,
        BankData bankData,
        string taxType,
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
        BankData = bankData;
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
            account.BankData,
            account.TaxType,
            account.TaxRate,
            account.CreatedAt
        );
}