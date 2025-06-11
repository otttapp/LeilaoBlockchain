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

// Controllers + ModelState customizado
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

// Swagger (mínimo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
});

// Logging
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Kestrel
builder.WebHost.UseKestrel();

// Banco de dados (EF com SQL Server)
builder.Services.AddDbContext<MainDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")!)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information);
});

// Serviços customizados
builder.Services.AddDbContext<MainDBContext>();
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();


// Middleware global
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

// Swagger em dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares essenciais
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
