﻿using Aparta.Authentication.Domain.Exceptions;
using Aparta.Authentication.UseCases.Exceptions;

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

        if (exception is NotFoundException)
        {
            details.Title = "Not Found";
            details.Status = StatusCodes.Status404NotFound;
            details.Type = "NotFound";
            details.Detail = exception!.Message;
        }

        else
        {
            details.Title = "One or more validation errors occurred";
            details.Status = StatusCodes.Status422UnprocessableEntity;
            details.Type = "UnprocessableEntity";
            details.Detail = exception!.Message;
        }

        context.HttpContext.Response.StatusCode = (int)details.Status;
        context.Result = new ObjectResult(details);
        context.ExceptionHandled = true;
    }
}