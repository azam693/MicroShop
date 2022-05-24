using Catalog.Domain.Exceptions;
using System.Net;

namespace Catalog.Api.Common.Middleware;

public class ExceptionMiddleware
{
    private RequestDelegate _requestDelegate;

    public ExceptionMiddleware(
        RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }
    
    public async Task InvokeAsync(HttpContext httpContext, ILogger logger)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);

            var statusCode = ex switch
            {
                CatalogException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            
            httpContext.Response.StatusCode = (int)statusCode;
        }
    }
}