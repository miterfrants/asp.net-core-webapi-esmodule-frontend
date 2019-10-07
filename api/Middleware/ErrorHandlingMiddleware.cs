using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Net;

using Newtonsoft.Json;

using Sample.Constants;
using Sample.Helpers;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context /* other dependencies */)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        string internalErrorMessage = ex.Message;
        if (ex.GetType() == typeof(CustomException))
        {
            var customEx = (CustomException)ex;
            if (customEx.code != HttpStatusCode.OK)
            {
                code = customEx.code;
            }

            internalErrorMessage = ErrorHelper.GetErrorMessageByCode(customEx.errorCode);
        }
        var result = new { status = CUSTOM_RESPONSE.STATUS.FAILED.ToString(), data = new { message = internalErrorMessage, stackTrace = ex.StackTrace } };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code.GetHashCode();
        return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}