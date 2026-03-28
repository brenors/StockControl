using Microsoft.AspNetCore.Mvc;
using StockControl.Common.Validator;
using System.Net;
using System.Text.Json;

namespace StockControl.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly JsonSerializerOptions options = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var problems = new List<ProblemDetails>();

                if (ex is DomainValidator domainValidator)
                {
                    context.Response.StatusCode = (int)domainValidator.Status;

                    if (domainValidator.Notifications.Any())
                    {
                        problems.AddRange(domainValidator.Notifications.Select(notification =>
                            new ProblemDetails
                            {
                                Title = "Validation",
                                Detail = notification,
                                Status = (int)domainValidator.Status
                            }));

                        _logger.LogWarning(ex, "Domain validation errors");
                    }
                    else
                    {
                        problems.Add(new ProblemDetails
                        {
                            Title = "Business rule",
                            Detail = domainValidator.Message,
                            Status = (int)domainValidator.Status
                        });

                        _logger.LogWarning(ex, "Business rule violation");
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    problems.Add(new ProblemDetails
                    {
                        Title = "Internal Server Error",
                        Detail = ex.Message,
                        Status = (int)HttpStatusCode.InternalServerError
                    });

                    _logger.LogError(ex, "Unhandled exception");
                }

                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(problems, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
