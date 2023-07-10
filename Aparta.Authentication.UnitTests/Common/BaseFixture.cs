﻿using Bogus;

namespace Aparta.Authentication.UnitTests.Common;

public abstract class BaseFixture
{
    public Faker Faker { get; set; }
    protected BaseFixture()
        => Faker = new Faker("pt_BR");
}
