using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LaBancaUCB.Api.Filters;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var response = new ErrorResponse
        {
            Status = (int)statusCode,
            Title = "Error Interno",
            Message = "Ha ocurrido un error inesperado. Por favor, intente más tarde.",
            TraceId = Guid.NewGuid().ToString() 
        };

        if (exception is BusinessException businessEx)
        {
            statusCode = businessEx.StatusCode;
            response.Status = (int)statusCode;
            response.Title = "Error de Regla de Negocio";
            response.Message = businessEx.Message;
            if (businessEx.Details != null)
            {
                response.Errors = new[] { businessEx.Details };
            }
        }
        else if (exception is ValidationException validationEx)
        {
            statusCode = HttpStatusCode.BadRequest;
            response.Status = (int)statusCode;
            response.Title = "Error de Validación";
            response.Message = "Se encontraron uno o más errores de validación.";
            response.Errors = validationEx.Errors.Select(e => new
            {
                Property = e.PropertyName,
                Error = e.ErrorMessage
            });
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResponse = JsonSerializer.Serialize(response, options);

        return context.Response.WriteAsync(jsonResponse);
    }
}