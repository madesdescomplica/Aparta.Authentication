﻿namespace Aparta.Authentication.Domain.Exceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException(string message) : base(message)
    {
    }
}