﻿using Aparta.Authentication.Domain.Enum;
using Aparta.Authentication.Domain.Exceptions;
using Validations = Aparta.Authentication.Domain.Validation;

using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Xunit;

namespace Aparta.Authentication.UnitTests.Domain.Validation.DomainValidation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(Should_Not_Throw_If_Receive_Correct_CPF))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Not_Throw_If_Receive_Correct_CPF()
    {
        var clientType = ClientType.PF;
        string documentNumber = Faker.Person.Cpf();
        
        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(Should_Throw_If_Receive_Incorrect_CPF))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Throw_If_Receive_Incorrect_CPF()
    {
        var clientType = ClientType.PF;
        string documentNumber = Faker.Person.Cpf() + "1";

        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid CPF number");
    }

    [Fact(DisplayName = nameof(Should_Not_Throw_If_Receive_Correct_CNPJ))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Not_Throw_If_Receive_Correct_CNPJ()
    {
        var clientType = ClientType.PJ;
        string documentNumber = Faker.Company.Cnpj();
        
        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(Should_Throw_If_Receive_Incorrect_CNPJ))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Throw_If_Receive_Incorrect_CNPJ()
    {
        var clientType = ClientType.PJ;
        string documentNumber = Faker.Company.Cnpj() + "1";
        
        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                clientType,
                documentNumber
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid CNPJ number");
    }

    [Fact(DisplayName = nameof(Should_Throw_If_Receive_Incorrect_ContentType))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Throw_If_Receive_Incorrect_ContentType()
    {
        var clientType = 3;
        string documentNumber = Faker.Company.Cnpj();
        
        Action action = ()
            => Validations.DomainValidation.ValidateCPFAndCNPJ(
                (ClientType)clientType,
                documentNumber
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid ClientType");
    }

    [Fact(DisplayName = nameof(Should_Not_Throw_If_Not_Receive_Null_Or_Empty))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void Should_Not_Throw_If_Not_Receive_Null_Or_Empty()
    {
        var target = Faker.Lorem.Paragraph().Replace(" ", "");
        string fieldName = Faker.Lorem.Word();

        Action action = ()
            => Validations.DomainValidation.NotNullOrEmpty(
                target, 
                fieldName
            );

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(Should_Throw_If_Receive_Null_Or_Empty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_If_Receive_Null_Or_Empty(string? target)
    {
        string fieldName = Faker.Lorem.Word();

        Action action = ()
            => Validations.DomainValidation.NotNullOrEmpty(
                target, 
                fieldName
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty or null");
    }

    [Theory(DisplayName = nameof(Should_Not_Throw_If_Not_Receive_MinLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesGreaterThanTheMin),
        parameters: 10,
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void Should_Not_Throw_If_Not_Receive_MinLength(string target, int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = ()
            => Validations.DomainValidation.MinLength(
                target, 
                minLength,
                fieldName
            );

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(Should_Throw_If_Receive_MinLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesSmallerThanTheMin),
        parameters: 10,
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void Should_Throw_If_Receive_MinLength(string target, int minLength)
    {
        string fieldName = Faker.Lorem.Word();

        Action action = ()
            => Validations.DomainValidation.MinLength(
                target, 
                minLength, 
                fieldName
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage(
                $"{fieldName} should have at least {minLength} characters"
            );
    }

    [Theory(DisplayName = nameof(Should_Not_Throw_If_Not_Receive_MaxLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesLessThanTheMax),
        parameters: 10,
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void Should_Not_Throw_If_Not_Receive_MaxLength(string target, int maxLength)
    {
        string fieldName = Faker.Lorem.Word();

        Action action = ()
            => Validations.DomainValidation.MaxLength(
                target, 
                maxLength, 
                fieldName
            );

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(Should_Throw_If_Receive_MaxLength))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesGreaterThanTheMax),
        parameters: 10,
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void Should_Throw_If_Receive_MaxLength(string target, int maxLength)
    {
        string fieldName = Faker.Lorem.Word();

        Action action = ()
            => Validations.DomainValidation.MaxLength(
                target, 
                maxLength, 
                fieldName
            );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage(
                $"{fieldName} should have less or equal {maxLength} characters"
            );
    }
}