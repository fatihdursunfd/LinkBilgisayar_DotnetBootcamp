using Assessment.Core.DTOs;
using Assessment.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Assessment.API.Middlewares
{
    public static class UseCustomExceptionHandler 
    {
        public static void UseCustomExcepiton(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400 ,
                        NotFoundException => 404 ,
                        _ => 500
                    };

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
