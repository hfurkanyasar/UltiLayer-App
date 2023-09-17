using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.MiddleWares
{
    public static class UseCustomExecpitonHandler
    {
        public static void userCustomExeption(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var execpitonFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = execpitonFeature.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500

                    };
                    context.Response.StatusCode = statusCode;


                    var response = CustomResponseDTO<NoContentDTO>.Fail
                    (statusCode, execpitonFeature.Error.Message);


                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });


            });

        }
    }
}
