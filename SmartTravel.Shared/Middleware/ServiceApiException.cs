using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartTravel.Shared.Logging;

namespace SmartTravel.Shared.Middleware
{
    public class ServiceApiException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var signedHeader = context.Response.Headers["API-Gateway"];
            string message = "";
            int statusCode = 0;
            string title = "";

            if (signedHeader.FirstOrDefault() is null)
            {
                title = "Error";
                message = "The service is currently unavailable. Please try again later.";
                statusCode = (int)HttpStatusCode.ServiceUnavailable;
                await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
                return;
            }
            else
            {
                try
                {
                    await next(context);

                    switch (context.Response.StatusCode)
                    {
                        case StatusCodes.Status400BadRequest:
                            message = "The request is invalid. Please check your input.";
                            statusCode = (int)HttpStatusCode.BadRequest;
                            await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
                            break;
                        case StatusCodes.Status405MethodNotAllowed:
                            message = "The HTTP method used is not allowed for this endpoint.";
                            statusCode = (int)HttpStatusCode.MethodNotAllowed;
                            await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    LoggingExtension.LogException(ex);
                }
            }
        }
    }
}
