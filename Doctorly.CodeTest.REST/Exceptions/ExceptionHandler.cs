using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Doctorly.CodeTest.REST.Exceptions
{
    public class ExceptionHandler
    {

        RequestDelegate Next { get; }
        ILogger<ExceptionHandler> Logger { get; }

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            Logger = logger;
            Next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await Next(httpContext);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            if (exception.Data.Count > 0)
            {
                IEnumerator key = exception.Data.Keys.GetEnumerator();
                if (key.MoveNext())
                    context.Response.StatusCode = (int)key.Current;
                else
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ExceptionDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = context.Response.StatusCode == (int)HttpStatusCode.InternalServerError ? $"Internal Server error: {exception.Message}" : exception.Message
            }.ToString());
        }
    }
}
