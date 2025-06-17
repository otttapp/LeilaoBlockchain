using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TesteAplicacao.Infraestructure.Context;
using TesteAplicacao.Infraestructure.Middleware;
using TesteAplicacao.Infraestructure.Repository;
using TesteAplicacao.Infraestrutra.Extensions;
using TesteAplicacao.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new ValidationFailedResult(context.ModelState);
        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        return result;
    };
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.WebHost.UseKestrel();

builder.Services.AddDbContext<MainDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")!)
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.RegisterServices();
builder.Services.RegisterRepositories();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services.AddTransient<HashAuthenticationMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<HashAuthenticationMiddleware>();
app.MapControllers();

app.Run();
