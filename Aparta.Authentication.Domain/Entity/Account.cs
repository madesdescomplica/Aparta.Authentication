﻿using Aparta.Authentication.Domain.Enum;
using Aparta.Authentication.Domain.SeedWork;

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
    }
}
