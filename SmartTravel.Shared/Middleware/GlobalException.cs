using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartTravel.Shared.Logging;

namespace SmartTravel.Shared.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //default error
            string message = "Internal server error occured. Kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";
            try
            {
                await next(context);

                switch (context.Response.StatusCode)
                {
                    case StatusCodes.Status404NotFound:
                        message = "The resource you are looking for could not be found.";
                        statusCode = (int)HttpStatusCode.NotFound;
                        await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
                        break;
                    case StatusCodes.Status401Unauthorized:
                        message = "You are not authorized to access this resource.";
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
                        break;
                    case StatusCodes.Status403Forbidden:
                        message = "Access to this resource is forbidden.";
                        statusCode = (int)HttpStatusCode.Forbidden;
                        await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
                        break;
                }
            }
            catch (Exception ex) 
            {
                LoggingExtension.LogException(ex);

                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    message = "The server timed out waiting for the request. Please try again!";
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                }

                await WriteExceptionResponse.ChangeHeader(context, title, message, statusCode);
            }

        }
    }
}
