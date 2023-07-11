﻿using Aparta.Authentication.Domain.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aparta.Authentication.API.Filters;

public class ApiGlobalExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _env;

    public ApiGlobalExceptionFilter(IHostEnvironment env)
        => _env = env;

    public void OnException(ExceptionContext context)
    {
        var details = new ProblemDetails();
        var exception = context.Exception;

        if (_env.IsDevelopment())
            details.Extensions.Add("StackTrace", exception.StackTrace);

        if (exception is EntityValidationException)
        {
            var ex = exception as EntityValidationException;
            details.Title = "One or more validation errors occurred.";
            details.Status = StatusCodes.Status422UnprocessableEntity;
            details.Detail = ex!.Message;
            details.Type = "UnprocessableEntity";
        }
        else
        {
            details.Title = "An unexpected error ocurred.";
            details.Status = StatusCodes.Status422UnprocessableEntity;
            details.Detail = exception.Message;
            details.Type = "Unexpected";
        }

        context.HttpContext.Response.StatusCode = details.Status.Value;
        context.Result = new ObjectResult(details);
        context.ExceptionHandled = true;
    }
}