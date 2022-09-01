using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Framework.Infrastructure
{

    public class CaptureExceptionMiddleware : IMiddleware, ITransientDependency
    {
      
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (TaskCanceledException ex)
            {
                await HandleExceptionAsync(context, 499,
                    "Request cancelled.", ex);
            }
            catch (OperationCanceledException ex)
            {
                await HandleExceptionAsync(context, 499,
                    "Request cancelled.", ex);
            }
            catch (BusinessException ex)
            {
                if (ex.Message.Contains("Tenant not found"))
                {
                    context.Response.Cookies.Delete("PolpAbpTenantId");
                }
                await HandleExceptionAsync(context, 499,
                                 "Outdated client information.", ex);
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