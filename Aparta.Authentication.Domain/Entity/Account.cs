using Aparta.Authentication.Domain.Enum;
using Aparta.Authentication.Domain.SeedWork;
using Aparta.Authentication.Domain.Validation;

namespace Aparta.Authentication.Domain.Entity;

public class Account : AggregateRoot
{
    public ClientType ClientType { get; private set; }
    public string DocumentNumber { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    public string BankName { get; private set; }
    public string AgencyNumber { get; private set; }
    public string AccountNumber { get; private set; }
    public TaxType TaxType { get; private set; }
    public float TaxRate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Account(
        ClientType clientType,
        string documentNumber,
        string name,
        string address,
        string phone,
        string bankName,
        string agencyNumber,
        string accountNumber,
        TaxType taxType,
        float taxRate
     ) : base()
    {
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
        CreatedAt = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.ValidateCPFAndCNPJ(ClientType, DocumentNumber);

        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, 3, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));

        DomainValidation.NotNullOrEmpty(Address, nameof(Address));

        DomainValidation.NotNullOrEmpty(Phone, nameof(Phone));

        DomainValidation.NotNullOrEmpty(BankName, nameof(BankName));
        DomainValidation.NotNullOrEmpty(AgencyNumber, nameof(AgencyNumber));
    }
}
