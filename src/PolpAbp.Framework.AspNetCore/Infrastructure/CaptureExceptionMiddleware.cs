using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Infrastructure
{

    public class CaptureExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CaptureExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (TaskCanceledException ex)
            {
                await HandleExceptionAsync(httpContext, 499,
                    "Request cancelled.", ex);
            }
            catch (OperationCanceledException ex)
            {
                await HandleExceptionAsync(httpContext, 499,
                    "Request cancelled.", ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, (int)HttpStatusCode.InternalServerError,
                    "Internal Server Error from the custom middleware.", ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, int statusCode, string message, Exception exception)
        {
            // todo: How to we set up the right content type???
            //context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }

}