﻿using Aparta.Authentication.Domain.Enum;

namespace Aparta.Authentication.API.ApiModels.Account;

public class UpdateAccountApiInput
{
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

    public UpdateAccountApiInput(
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
     )
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
    }
}