using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace TransactionImporter.Rest.Extensions
{
    public static class GlobalExceptionHandler
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app
                                                    , ILogger logger
                                                    , bool respondWithJsonErrorDetails = false)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    //Log occured Exception
                    var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

                    string errorDetails = $@"{exception.Message}
                                             {Environment.NewLine}
                                             {exception.StackTrace}";

                    int statusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.StatusCode = statusCode;

                    var problemDetails = new ProblemDetails
                    {
                        Title = "Unexpected Error",
                        Status = statusCode,
                        Detail = errorDetails,
                        Instance = Guid.NewGuid().ToString()
                    };

                    var json = JsonConvert.SerializeObject(problemDetails);

                    logger.LogError(json);

                    //Return response with error details
                    context.Response.ContentType = "application/json; charset=utf-8";

                    if (!respondWithJsonErrorDetails)
                        json = JsonConvert.SerializeObject(new { Title = "Unexpected Error", Status = statusCode });

                    await context.Response.WriteAsync(json, Encoding.UTF8);
                });
            });
        }
    }
}
