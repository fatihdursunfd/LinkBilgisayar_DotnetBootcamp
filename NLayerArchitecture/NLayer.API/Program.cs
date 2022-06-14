using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NLayer.Data;
using NLayer.Data.Interfaces;
using NLayer.Data.Models;
using NLayer.Data.Repositories;
using NLayer.Service;
using NLayer.Service.Interfaces;
using NLayer.Service.Models;
using NLayer.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("NLayerDBConnString") , action =>
    {
        action.MigrationsAssembly("NLayer.Data");
    } );
});

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IProductFeatureRepo, ProductFeatureRepo>();


builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductFeatureService, ProductFeatureService>();
builder.Services.AddScoped<IFullProductService, FullProductService>();

builder.Services.AddScoped<UnitOfWork>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<ExceptionHandlerFeature>();
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync<Response<string>>(new Response<string>()
        { 
            Data = null, 
            Errors = new List<string>() { exceptionFeature!.Error.Message }, 
            StatusCode = 500 
        }); 
    });
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
